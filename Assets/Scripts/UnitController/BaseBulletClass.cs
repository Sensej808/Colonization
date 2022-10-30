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
        MoveBullet();
        DestroyBullet();
    }
    public void MoveBullet()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public void DestroyBullet()
    {
        if (transform.position == target.transform.position || target == null)
            Destroy(gameObject);
    }
}
