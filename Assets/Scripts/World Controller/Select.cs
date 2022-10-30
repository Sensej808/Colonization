using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ��� ������ ����� ���������
public class Select : MonoBehaviour
{
    public bool k = false; //�� ����� ����� �� ��� ����� � ���� �� ��������
    private GameObject[] arrAlliedUnits; //������ ������� ������
    private void Start()
    {
        DeleteSelected();
    }
    private void OnTriggerEnter2D(Collider2D hitInfo) //���� ����� ������� �� �����, �������� ���
    {
        SelectionCheck allyUnit = hitInfo.GetComponent<SelectionCheck>();
        if (allyUnit)
        {
            allyUnit.isSelected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //���� ����� ������� �� �����, ������� ��� �� ���������
    {
        SelectionCheck allyUnit = collision.GetComponent<SelectionCheck>();
        if (allyUnit && k)
            allyUnit.isSelected = false;
    }
    private void DeleteSelected() //������ isSelected = false ���� ������, ��� �������� ����� �����
    {
        arrAlliedUnits = GameObject.FindGameObjectsWithTag("Allied");
        foreach (GameObject en in arrAlliedUnits)
        {
            if (en != null)
            {
                SelectionCheck Selection = en.GetComponent<SelectionCheck>();
                Selection.isSelected = false;
            }
        }
    }
}
