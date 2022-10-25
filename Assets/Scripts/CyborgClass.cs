using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CyborgClass : BaseUnitClass
{

    public GameObject bullet;
    public GameObject targetUnit;
    public bool isAttack;
    public new void Start()
    {
        base.Start();
        //своё
    }
    public void CreateBullet()
    {
        bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Circle"), transform.position, transform.rotation);
        targetUnit = TargetUnit();
    }
    public void MoveBullet()
    {
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetUnit.transform.position, 10f * Time.deltaTime);
        if (bullet.transform.position == targetUnit.transform.position)
        {
            Destroy(bullet);
            isAttack = false;
        }
    }
    public new void Attack()
    {
        if(bullet == null)
            CreateBullet();
        if (bullet != null)
            MoveBullet();

    }
    public new void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey("a") && Selection.isSelected)
            isAttack = true;
        if (isAttack)
            Attack();
    }
}
