using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CyborgClass : BaseUnitClass
{
    public new void Start()
    {
        base.Start();
        attack_radius = 5f;
        //своё
    }
    public void CreateBullet()
    {
        if (tUnit != null && goAttack)
        {
            Moving.isMoving = false;
            bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Circle"), transform.position, transform.rotation);
        }
    }
    public void MoveBullet()
    {
        if (realTarget == null)
            realTarget = tUnit;
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, realTarget.transform.position, 3f * Time.deltaTime);
        if (bullet.transform.position == realTarget.transform.position)
        {
            Destroy(bullet);
            isAttack = false;
        }
    }
    public new void Attack()
    {
        if (bullet == null)
            CreateBullet();
        if (bullet != null)
            MoveBullet();

    }
    public new void Update()
    {
            tUnit = TargetUnit();
            if (Input.GetMouseButton(0) && Input.GetKey("a") && Selection.isSelected)
            {
                realTarget = null;
                goAttack = true;
                Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Moving.finalPos.z = 0;
                if (bullet == null)
                    Moving.isMoving = true;
                isAttack = true;
            }
            if (Input.GetMouseButton(1) && Selection.isSelected)
            {
                goAttack = false;
            }
            if (tUnit != null && Moving.isMoving == false);
            {
                Attack();
            }
    }
}
