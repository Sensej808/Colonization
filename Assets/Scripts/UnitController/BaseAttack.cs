using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


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
    void Start()
    {
        unit = gameObject.GetComponent<BaseUnitClass>();
        realCooldown = 0f;
        GetTargetRange = 7.5f;
        isFocusAttack = false;
        finalAttackPos = gameObject.transform.position;

    }
    public GameObject SetNearestTarget()
    {
        float min_dist = float.MaxValue;
        GameObject nearest_unit = null;
        GameObject[] arrEnemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
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
            if ((transform.position - target.transform.position).magnitude <= attackRange)
            {
                if (realCooldown <= 0f)
                {
                    bullet = Instantiate(bulletPattern, transform.position, transform.rotation);
                    realCooldown = cooldown;
                    BaseBulletClass bbc = bullet.GetComponent<BaseBulletClass>();
                    bbc.target = target;
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
            unit.Moving.isMoving = true;
            unit.Moving.finalPos = target.transform.position;
        }
        else
            CreateBullet();
    }
    void Update()
    {
        if (realCooldown >= 0)
            realCooldown -= 0.1f;
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
            if (finalAttackPos != gameObject.transform.position && unit.state == StateUnit.Aggressive)
            {
                unit.Moving.isMoving = true;
                unit.Moving.finalPos = finalAttackPos;
            }
            if (!isFocusAttack && ((!unit.Moving.isMoving && unit.state == StateUnit.Normal) || (unit.state == StateUnit.Aggressive)))
                target = ConstantSearchEnemy();
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
        }
    }
}
