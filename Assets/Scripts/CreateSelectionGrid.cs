using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//������� ����� ��������� ������
public class CreateSelectionGrid : MonoBehaviour
{
    public GameObject prefabSelectionGrid; //������ ����� ���������
    private Vector3 pos1; //������� ���� �� ������, ��� ������ ������� ���
    private Vector3 pos2; //������� ���� �� ������, �� ����� ������������� ����� ���������
    private Vector3 rpos1;//������� ���� �� �����, ��� ������ ������� ���
    private Vector3 rpos2;//������� ���� �� �����, �� ����� ������������� ����� ���������
    private Vector3 posc; //����� ����� ���������
    private float length; //����� ����� ���������
    private float width;  //������ ����� ���������
    private bool gridIsBe = false; //false - ���� ����� ������ ���, true - ���� ����� �������
    private GameObject realSelectionGrid; //����� ��������� �� ������
    void Update()
    {
        if (Input.GetMouseButton(0)) //����� �������� ��� ������� ���
        {
            if (!gridIsBe)
            {
                pos1 = Input.mousePosition;
                pos2 = pos1;
            }
            else
                pos2 = Input.mousePosition;
            CenterPos();
            rpos1 = Camera.main.ScreenToWorldPoint(pos1); //������� ���� �� �����, ��� ������ ������� ���
            rpos2 = Camera.main.ScreenToWorldPoint(pos2); //������� ���� �� �����, �� ����� ������������� ����� ���������
            length = (rpos2.x - rpos1.x);
            width = (rpos1.y - rpos2.y);
            if (!gridIsBe)
            {
                realSelectionGrid = Instantiate(prefabSelectionGrid, posc, transform.rotation);
                var s = realSelectionGrid.GetComponent<Select>(); //������ ������ �� ����� ����� ���, �� ��� ����� ������-�� �� ��������
                s.k = true;
            }
            realSelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);
            realSelectionGrid.transform.localScale = new Vector3(length, width, pos1.z);
            gridIsBe = true;
        }
        else
        {
            gridIsBe = false;
            if (realSelectionGrid != null)
            {
                var s = realSelectionGrid.GetComponent<Select>(); //���� �����, ��� ����, �� �����
                s.k = false;
            }
            Destroy(realSelectionGrid);
        }
    }
    void CenterPos()//��������, ������ ����� ����� ���������
    {
        posc.x = pos2.x - (pos2.x - pos1.x) / 2;
        posc.y = pos1.y - (pos1.y - pos2.y) / 2;
        posc.z = pos1.z;
        posc = Camera.main.ScreenToWorldPoint(posc);
        posc.z = pos1.z;
    }
}
