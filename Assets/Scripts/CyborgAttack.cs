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
        unit.TakeTargetUnit.isAttack = false;
    }
    public void CreateBullet()
    {
        unit.TakeTargetUnit.bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Circle"), transform.position, transform.rotation);
    }
    public void MoveBullet()
    {
        unit.TakeTargetUnit.bullet.transform.position = Vector3.MoveTowards(unit.TakeTargetUnit.bullet.transform.position, unit.TakeTargetUnit.TargetUnit.transform.position, 3f * Time.deltaTime);
        if (unit.TakeTargetUnit.bullet.transform.position == unit.TakeTargetUnit.TargetUnit.transform.position)
            Destroy(unit.TakeTargetUnit.bullet);
    }
    public void Attack()
    {
        if (unit.TakeTargetUnit.TargetUnit != null)
        {
            if ((unit.TakeTargetUnit.TargetUnit.transform.position - transform.position).magnitude <= unit.TakeTargetUnit.attackRadius)
            {
                if (unit.TakeTargetUnit.bullet == null)
                    CreateBullet();
                else
                    MoveBullet();
                unit.Moving.isMoving = false;
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey("a") && unit.Selection.isSelected)
        {
            unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            unit.Moving.finalPos.z = 0;
            unit.Moving.isMoving = true;
            unit.TakeTargetUnit.TargetUnit = unit.TakeTargetUnit.SetTargetUnit();
        }
        if (Input.GetMouseButton(1))
        {
            unit.TakeTargetUnit.TargetUnit = null;
            unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            unit.Moving.finalPos.z = 0;
            unit.Moving.isMoving = true;
        }
        Attack();
    }
}
