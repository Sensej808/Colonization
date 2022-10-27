using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CyborgAttack : MonoBehaviour
{
    public CyborgClass unit;
    void Start()
    {
        unit = gameObject.GetComponent<CyborgClass>();
        unit.TakeTargetUnit.attackRadius = 5f;
        unit.TakeTargetUnit.goAttack = false;
        unit.TakeTargetUnit.cooldown = 100f;
        unit.TakeTargetUnit.realCooldown = 0f;
    }
    public void CreateBullet()
    {
        unit.TakeTargetUnit.bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Circle"), transform.position, transform.rotation);
        unit.Moving.isMoving = false;
    }
    public void MoveBullet()
    {
        if (unit.TakeTargetUnit.realTargetUnit == null)
            unit.TakeTargetUnit.realTargetUnit = unit.TakeTargetUnit.TargetUnit;
        unit.TakeTargetUnit.bullet.transform.position = Vector3.MoveTowards(unit.TakeTargetUnit.bullet.transform.position, unit.TakeTargetUnit.realTargetUnit.transform.position, 3f * Time.deltaTime);
        if (unit.TakeTargetUnit.bullet.transform.position == unit.TakeTargetUnit.realTargetUnit.transform.position)
        {
            Destroy(unit.TakeTargetUnit.bullet);
            unit.TakeTargetUnit.realTargetUnit = null;
        }
    }
    public void Attack()
    {
        if (unit.TakeTargetUnit.goAttack && unit.TakeTargetUnit.bullet == null &&(transform.position - unit.TakeTargetUnit.TargetUnit.transform.position).magnitude <= unit.TakeTargetUnit.attackRadius && unit.TakeTargetUnit.realCooldown <= 0f)
        {
            CreateBullet();
            unit.TakeTargetUnit.realCooldown = unit.TakeTargetUnit.cooldown;
        }
        if (unit.TakeTargetUnit.bullet != null)
            MoveBullet();

    }
    void Update()
    {
        if (unit.TakeTargetUnit.realCooldown >= 0)
            unit.TakeTargetUnit.realCooldown -= 0.1f;
        if (Input.GetMouseButtonDown(0) && Input.GetKey("a") && unit.Selection.isSelected)
        {
            unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            unit.Moving.finalPos.z = 0;
            if (unit.TakeTargetUnit.bullet == null)
                unit.Moving.isMoving = true;
            unit.TakeTargetUnit.TargetUnit = unit.TakeTargetUnit.SetTargetUnit();
            unit.TakeTargetUnit.goAttack = true;
        }
        if ((Input.GetKeyDown("s") || Input.GetMouseButtonDown(1)) && unit.Selection.isSelected)
        {
            unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            unit.Moving.finalPos.z = 0;
            unit.Moving.isMoving = true;
            unit.TakeTargetUnit.goAttack = false;
        }
        Attack();
    }
}
