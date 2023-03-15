using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Базовый класс снарядов атаки юнитов
public class BaseBulletClass : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public double damage;
    void Start()
    {
    }
    public virtual void Update()
    {
        DestroyBullet();
        MoveBullet();
    }
    public void MoveBullet()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public void DestroyBullet()
    {
        if (target != null)
        {
            if (transform.position == target.transform.position)
            {
                target.GetComponent<Health>().GetDamage(damage);
                Destroy(gameObject);
            }
            else if ((transform.position - target.transform.position).magnitude < 0.1f)
            {
                target.GetComponent<Health>().GetDamage(damage);
                Destroy(gameObject);
            }
        }
        if (target == null)
            Destroy(gameObject);
    }
}
