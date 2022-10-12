using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionWithStruct : MonoBehaviour
{
    private int i = 0;//����� ��� ����������� �������������� ������� ������ � ��� ������������
    public void OnTriggerExit2D(Collider2D collision)
    {
        //����� ��� ����������� �������������� ������� ������ � ��� ������������
        DoUnits struction = collision.GetComponent<DoUnits>();
        if (struction)
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
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        DoUnits struction = hitInfo.GetComponent<DoUnits>();
        if (struction)
        {
            i++;
            gameObject.tag = "IsNotFreePosition";//��� ������� � ��� ��� ����� �������
            //������ ����
            gameObject.transform.Find("Body").gameObject.GetComponent<Renderer>().material.color = new Vector4(251 / 255.0f, 15 / 255.0f, 15 / 255.0f, 1);
        }
    }
}
