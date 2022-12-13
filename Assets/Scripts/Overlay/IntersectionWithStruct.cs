using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class IntersectionWithStruct : MonoBehaviour
{
    private int i = 0;//����� ��� ����������� �������������� ������� ������ � ��� ������������
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MyTerritory")
        {
            i--;
        }
        if (i == 0)
        {
            gameObject.tag = "IsFreePosition";//��� ������� � ��� ��� ����� ��������
            //������ ����
            gameObject.transform.Find("Body").gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MyTerritory")
        {
            i++;
            gameObject.tag = "IsNotFreePosition";//��� ������� � ��� ��� ����� �������
            //������ ����
            gameObject.transform.Find("Body").gameObject.GetComponent<Renderer>().material.color = new Vector4(251 / 255.0f, 15 / 255.0f, 15 / 255.0f, 1);
        }
    }
    public void Update()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
    }
}
