using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


//Базовый класс атаки юнитов
public class BaseAttack : MonoBehaviour
{
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
        GetTargetRange = 7.5f;
        isFocusAttack = false;
        finalAttackPos = gameObject.transform.position;
        k = 50;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartTimer());
        timerRun = true;
    }
    public GameObject SetNearestTarget()
    {
        float min_dist = float.MaxValue;
        GameObject nearest_unit = null;
        //GameObject[] arrEnemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> arrEnemyUnits = new List<GameObject>(GameObject.FindObjectsOfType<GameObject>());
        arrEnemyUnits = arrEnemyUnits.FindAll(x => x.tag != gameObject.tag && x.GetComponent<Health>());
        foreach (GameObject unit in arrEnemyUnits)
        {
            if (unit != null)
            {
                float real_dist = (gameObject.transform.position - unit.transform.position).magnitude;
                if (real_dist < min_dist)
                {
                    min_dist = real_dist;
                    nearest_unit = unit;
                }
            }
        }
        return nearest_unit;
    }
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
    public GameObject SetTargetUnit()
    {
        GameObject target = SetFocusTarget();
        if (target == null)
            target = SetNearestTarget();
        return target;
    }
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
    public GameObject ConstantSearchEnemy()
    {
        GameObject target = SetNearestTarget();
        if (target != null)
        {
            if ((target.transform.position - gameObject.transform.position).magnitude <= GetTargetRange)
            {
                unit.state = StateUnit.Aggressive;
                return target;
            }
            else
                return null;
        }
        return null;
    }
    public void GoAttackAndAttack()
    {
        if ((gameObject.transform.position - target.transform.position).magnitude >= attackRange)
        {
            if (!unit.Moving.isMoving)
                unit.Moving.MoveTo(target.transform.position);
        }
        else
            CreateBullet();
    }
    void Update()
    {
        if (unit.state != StateUnit.BuildStruct)
        {
            if (Input.GetMouseButtonDown(0) && Input.GetKey("a") && unit.Selection.isSelected && !EventSystem.current.IsPointerOverGameObject())
            {
                target = SetFocusTarget();
                if (target != null)
                    isFocusAttack = true;
                unit.state = StateUnit.Aggressive;
                finalAttackPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                finalAttackPos.z = 0;
            }
            if (finalAttackPos != gameObject.transform.position && unit.state == StateUnit.Aggressive && target == null && !unit.Moving.isMoving)
                unit.Moving.MoveTo(finalAttackPos);
            if ((!unit.gameObject.GetComponent<Build>() && !isFocusAttack && ((!unit.Moving.isMoving && unit.state == StateUnit.Normal) || (unit.state == StateUnit.Aggressive)) || (gameObject.GetComponent<Build>() && unit.state == StateUnit.Aggressive)))
            {
                if (target != ConstantSearchEnemy() && k <= 0)
                {
                    target = ConstantSearchEnemy();
                    unit.Moving.isMoving = false;
                    k = 50;
                }
            }
            if (Input.GetMouseButtonDown(1) && unit.Selection.isSelected)
            {
                unit.state = StateUnit.Normal;
                target = null;
                finalAttackPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                finalAttackPos.z = 0;
            }
            if (target != null && unit.state == StateUnit.Aggressive)
                GoAttackAndAttack();
            if (target == null || target.activeSelf == false)
                isFocusAttack = false;
            if (target == null && (transform.position - finalAttackPos).magnitude <= 1f)
                unit.state = StateUnit.Normal;
        }
        if (k >= 0)
           k--;
    }
}
