using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletClass : MonoBehaviour
{
    public GameObject targetUnit;
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
        transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, speed * Time.deltaTime);
    }
    public void DestroyBullet()
    {
        if (transform.position == targetUnit.transform.position || targetUnit == null)
            Destroy(gameObject);
    }
}
