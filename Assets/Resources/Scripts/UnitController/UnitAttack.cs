using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����������� ������ ���������
//����������� ��������
[RequireComponent(typeof(CircleCollider2D))]
public class UnitAttack : MonoBehaviour
{
    CircleCollider2D vision;
    GameObject target;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Allied")
        {
            target = collision.gameObject;
            Debug.Log($"Allied in coll: {target.name}");
        }
    }

}
