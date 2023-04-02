using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;

//Базовый класс атаки юнитов
public class BaseAttack : MonoBehaviour
{
    [SerializeReference]
    List<GameObject> targets; //Потенциальные цели в зоне видимости
    CircleCollider2D AttackRange;
    public BaseUnitClass unit;
    public float attackRange;
    public GameObject target;
    public GameObject bullet;
    public float cooldown;
    public float realCooldown;
    public GameObject bulletPattern;
    public float GetTargetRange;
    public bool isFocusAttack;
    public Vector3 finalAttackPos;
    public int k;
    AudioSource audioSource;
    public AudioClip shoot;
    public bool timerRun;
    public bool isNormalMoving;
    public bool isCalled;

    //Обычный таймер ставит timerRun = false по окончании
    private IEnumerator StartTimer()
    {
        while (realCooldown >= -0.1f)
        {
            realCooldown -= Time.deltaTime;
            yield return null;
        }
        timerRun = false;
    }
    void Start()
    {
        unit = gameObject.GetComponent<BaseUnitClass>();
        realCooldown = 0f;
        //GetTargetRange = 7.5f;
        isFocusAttack = false;
        finalAttackPos = gameObject.transform.position; //Точка, откуда ведется атака
        k = 50;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartTimer());
        timerRun = true;
        isCalled = false;
    }

    //Находит ближайшую цель в радиусе GetTargetRange, возвращает найденную цель
    public GameObject FindNearestTarget()
    {
        float min_dist = float.MaxValue;
        GameObject nearest_unit = null;
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, GetTargetRange);
        foreach (Collider2D unit in hitColiders)
        {
            if (unit.gameObject != null && unit.gameObject.tag != gameObject.tag && unit.gameObject.GetComponent<Health>() != null)
            {
                float real_dist = (gameObject.transform.position - unit.gameObject.transform.position).magnitude;
                if (real_dist < min_dist)
                {
                    min_dist = real_dist;
                    nearest_unit = unit.gameObject;
                }
            }
        }
        return nearest_unit;
    }

    //Возвращает потенциальную цель(target) по позиции мышки. Null, если целей нет. Что?
    public GameObject SetFocusTarget()
    {
        GameObject result = null;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Collider2D hitColider = Physics2D.OverlapCircle(pos, 0.1f);
        if (hitColider != null)
        {
            if (hitColider.gameObject.tag == "Enemy")
                result = hitColider.gameObject;
        }
        return result;
    }

    //Выстрел!
    public void CreateBullet()
    {
        if (target != null)
        {
            if (!timerRun)
            {
                realCooldown = 0;
                StartCoroutine(StartTimer());
                timerRun = true;
            }
            if ((transform.position - target.transform.position).magnitude <= attackRange)
            {
                if (realCooldown <= 0f)
                {
                    bullet = Instantiate(bulletPattern, transform.position, transform.rotation);
                    realCooldown = cooldown;
                    BaseBulletClass bbc = bullet.GetComponent<BaseBulletClass>();
                    bbc.target = target;
                    audioSource.PlayOneShot(shoot);
                    //Debug.Log($"Played sound: {shoot}, {shoot.loadState}");
                }
                unit.Moving.isMoving = false;
            }
        }
    }

    //Передвигает юнита к цели, на растояние AttackRange
    public void MoveToTarget()
    {

        Debug.Log("GoToBOrd");
        Queue<PathNode> FinalPoses = new Queue<PathNode>();
        PathFinding.Instance.grid.GetXY(target.transform.position - (target.transform.position - transform.position).normalized * (attackRange - PathFinding.Instance.grid.CellSize), out int x, out int y);
        FinalPoses.Enqueue(PathFinding.Instance.grid.GetValue(x, y));

        List<PathNode> aims = new List<PathNode>();

        while (FinalPoses.Count != 0)
        {
            var p = FinalPoses.Dequeue();
            var neighs = PathFinding.Instance.OpenNeighbours(p);
            foreach (var n in neighs)
            {
                if (n.is_walkable && (PathFinding.Instance.grid.GetWorldPos(n) - target.transform.position).magnitude < attackRange - PathFinding.Instance.grid.CellSize)
                {
                    FinalPoses.Enqueue(n);

                }

            }
            bool is_taken = false;
            if (unit.gameObject.layer == 9) //Наземные юниты
                is_taken = Physics2D.OverlapBoxAll(PathFinding.Instance.grid.GetWorldPos(p), Vector2.one * p.grid.CellSize, 0, LayerMask.GetMask("GroundUnits")).All((col) => col.isTrigger);
            else
                is_taken = Physics2D.OverlapBoxAll(PathFinding.Instance.grid.GetWorldPos(p), Vector2.one * p.grid.CellSize, 0, LayerMask.GetMask("Air")).All((col) => col.isTrigger);
            if (PathFinding.Instance.grid.GetValue(p.x, p.y).is_empty && is_taken)
            {
                unit.GetComponent<AllyMoving>().MoveTo(PathFinding.Instance.grid.GetWorldPos(p.x, p.y));
                aims.Add(p);
                p.is_empty = false;
                break;
            }
        }
        foreach (var node in aims)
        {
            node.is_empty = true;
        }
    }

    //Поведение при атаке
    public void GoAttackAndAttack()
    {
        if ((gameObject.transform.position - target.transform.position).magnitude >= attackRange)
        {
            if (!unit.Moving.isMoving)
            {
                //if (target.GetComponent<Rigidbody2D>() != null)
                {
                    if (target.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
                        unit.Moving.MoveTo(target.transform.position);
                    else
                    {
                        //unit.Moving.MoveTo(target);
                        //unit.Moving.MoveTo(target.transform.position - new Vector3(3, 3, 0));
                    }
                }
                //else
                //    unit.Moving.MoveTo(target);
            }
        }
        else
            CreateBullet();
    }

    //Это что?
    public void DoIsMoving()
    {
        isNormalMoving = false;
    }

    //Позвать союзных юнитов в радиусе GetTargetRange на помощь
    public void CallToHelp()
    {
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, GetTargetRange);
        foreach (Collider2D unit in hitColiders)
        {
            if(unit.gameObject.GetComponent<BaseAttack>() != null && unit.gameObject.GetComponent<Build>() == null)
            {
                if (unit.gameObject.GetComponent<BaseAttack>().target == null && unit.gameObject.tag == "Enemy")
                {
                    if (target.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
                    {
                        unit.gameObject.GetComponent<BaseAttack>().finalAttackPos = target.transform.position;
                        unit.gameObject.GetComponent<BaseUnitClass>().state = StateUnit.Aggressive;
                    }
                }
            }
        }
    }
    private void Update()
    {
        if(target == null && targets.Count != 0 && !unit.Moving.isMoving)
        {
            target = targets.First();
           
        }
       if(target != null)
       {
            if (!isCalled && gameObject.tag == "Enemy")
            {
                CallToHelp();
                isCalled = true;
            }
            if (!unit.Moving.isMoving)
            {
                Debug.Log($"{(target.transform.position - transform.position).magnitude} - {attackRange - PathFinding.Instance.grid.CellSize}");
                if ((target.transform.position - transform.position).magnitude > attackRange - PathFinding.Instance.grid.CellSize)
                    MoveToTarget();
                else
                    CreateBullet();

            }

       }

        /*if (target != null)
        {
            if ((target.transform.position - gameObject.transform.position).magnitude > attackRange && ((!unit.gameObject.GetComponent<Build>() && isNormalMoving == false && unit.state == StateUnit.Normal && isFocusAttack == false) || (unit.state == StateUnit.Aggressive)))
            {
                //target = FindNearestTarget();
            }
        }
        else
        {
            if ((!unit.gameObject.GetComponent<Build>() && isNormalMoving == false && unit.state == StateUnit.Normal && isFocusAttack == false) || (unit.state == StateUnit.Aggressive))
            {
                //target = FindNearestTarget();
            }
        }
        if (target != null)
        {
            
            GoAttackAndAttack();
           
        }
        if (Input.GetMouseButtonDown(1) && unit.Selection.isSelected && unit.state != StateUnit.BuildStruct)
        {
            target = null;
            unit.state = StateUnit.Normal;
            isFocusAttack = false;
            isNormalMoving = true;
            var x = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            x.z = 0;
            //unit.Moving.MoveTo(x);
        }
        if (unit.Selection.isSelected && Input.GetMouseButtonDown(0) && Input.GetKey("a"))
        {
            target = SetFocusTarget();
            if (target == null)
            {
                target = FindNearestTarget();
                finalAttackPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                finalAttackPos.z = 0;
                unit.state = StateUnit.Aggressive;
            }
            else
                isFocusAttack = true;
        }
        if (finalAttackPos != gameObject.transform.position && unit.state == StateUnit.Aggressive && target == null && !unit.Moving.isMoving)
            unit.Moving.MoveTo(finalAttackPos);
        if (target == null && (transform.position - finalAttackPos).magnitude <= 1f)
            unit.state = StateUnit.Normal;
        if (target == null)
        {
            isFocusAttack = false;
            isCalled = false;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != gameObject.tag && !collision.isTrigger && collision.gameObject.GetComponent<SourseOfRecourses>() == null)
        {
            targets.Add(collision.gameObject);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != gameObject.tag && !collision.isTrigger && collision.gameObject.GetComponent<SourseOfRecourses>() == null)
        {
            targets.Remove(collision.gameObject);
            if (target == collision.gameObject) //Если текущая цель вышла из зоны видимости
                target = null;

        }
    }
}
