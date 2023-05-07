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

    List<GameObject> Targets;
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
        finalAttackPos = gameObject.transform.position;
        k = 50;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartTimer());
        timerRun = true;
        isCalled = false;
    }
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
                        unit.Moving.MoveTo(target.transform.position - new Vector3(3, 3, 0));
                    }
                }
                //else
                //    unit.Moving.MoveTo(target);
            }
        }
        else
            CreateBullet();
    }
    public void DoIsMoving()
    {
        isNormalMoving = false;
    }
    public void CallToHelp()
    {
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, GetTargetRange);
        foreach (Collider2D unit in hitColiders)
        {
            if (unit.gameObject.GetComponent<BaseAttack>() != null && unit.gameObject.GetComponent<Build>() == null)
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
        if (target != null)
        {
            if ((target.transform.position - gameObject.transform.position).magnitude > attackRange && ((!unit.gameObject.GetComponent<Build>() && isNormalMoving == false && unit.state == StateUnit.Normal && isFocusAttack == false) || (unit.state == StateUnit.Aggressive)))
            {
                target = FindNearestTarget();
            }
        }
        else
        {
            if ((!unit.gameObject.GetComponent<Build>() && isNormalMoving == false && unit.state == StateUnit.Normal && isFocusAttack == false) || (unit.state == StateUnit.Aggressive))
            {
                target = FindNearestTarget();
            }
        }
        if (target != null)
        {
            GoAttackAndAttack();
            if (!isCalled && gameObject.tag == "Enemy")
            {
                CallToHelp();
                isCalled = true;
            }
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
        }
    }

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (Targets.Contains(col.gameObject))
    //    {
    //        Targets.Remove(col.gameObject);
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if(col.tag == "Enemy")
    //    {
    //        Targets.Add(col.gameObject);
    //    }
    //}
}
