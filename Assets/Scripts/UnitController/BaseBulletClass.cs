using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Базовый класс снарядов атаки юнитов
public class BaseBulletClass : MonoBehaviour
{
    public GameObject target;
    public float speed;
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
                Destroy(gameObject);
        }
        if (target == null)
            Destroy(gameObject);
    }
}
