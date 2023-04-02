using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : BaseBulletClass
{
    public double additionalDamage;
    void Start()
    {
        speed = 10f;
        damage = 70;
        additionalDamage = damage/4*3;
    }
    new void Update()
    {
        base.Update();
    }
    public void OnDestroy()
    {
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, 2);
        foreach (Collider2D unit in hitColiders)
        {
            if (unit.gameObject != null && unit.gameObject.tag != gameObject.tag && unit.gameObject.GetComponent<Health>() != null)
            {
                unit.gameObject.GetComponent<Health>().GetDamage(additionalDamage);
            }
        }

    }
}
