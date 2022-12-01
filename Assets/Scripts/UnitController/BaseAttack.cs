using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


//������� ����� ����� ������
public class BaseAttack : MonoBehaviour
{
    public BaseUnitClass unit;
    public float attackRange;
    public GameObject target;
    public GameObject bullet;
    public bool goAttack;
    public float cooldown;
    public float realCooldown;
    public GameObject bulletPattern;
    void Start()
    {
        unit = gameObject.GetComponent<BaseUnitClass>();
        goAttack = false;
        realCooldown = 0f;
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
            if (goAttack && (transform.position - target.transform.position).magnitude <= attackRange)
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
    void Update()
    {
        if (unit.state != StateUnit.BuildStruct)
        {
            if (realCooldown >= 0)
                realCooldown -= 0.1f;
            if (Input.GetMouseButtonDown(0) && Input.GetKey("a") && unit.Selection.isSelected && !EventSystem.current.IsPointerOverGameObject())
            {
                unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                unit.Moving.finalPos.z = 0;
                if (bullet == null)
                    unit.Moving.isMoving = true;
                target = SetTargetUnit();
                if (target != null)
                {
                    if ((transform.position - target.transform.position).magnitude >= attackRange)
                        unit.Moving.isMoving = true;
                }
                goAttack = true;
            }
            if ((Input.GetKeyDown("s") || Input.GetMouseButtonDown(1)) && unit.Selection.isSelected && !EventSystem.current.IsPointerOverGameObject())
            {
                unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                unit.Moving.finalPos.z = 0;
                unit.Moving.isMoving = true;
                goAttack = false;
            }
            if (target != null)
            {
                if (goAttack && (transform.position - target.transform.position).magnitude < attackRange)
                    unit.Moving.isMoving = false;
            }
            if (goAttack && transform.position == unit.Moving.finalPos)
                goAttack = false;
            CreateBullet();
        }
    }
}