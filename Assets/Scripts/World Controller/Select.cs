using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TODO: ������� ��������� ��������� ������ �������� �� ������
//TODO: ������������ ��������� ������ ������ ����� ���������� ���������(�������� �����), � �� �����, �����, ����� ����������
//������ ��� ������ ����� ���������
public class Select : MonoBehaviour
{
    //��� ���������� ��������� ����� ��������� ��������� � �������� OnTriggerExit2D ��� ���� ��������.
    //����� ��� ���������� ������� ��������, ��������� IsDone, ������� ����� true ��� ��������
    public bool IsDone = false; 
    public InGameStorage storage;
    private void Start()
    {
        storage = GameObject.Find("Storage").GetComponent<InGameStorage>();
        DeleteSelected();
    }
    private void OnTriggerEnter2D(Collider2D hitInfo) //���� ����� ������� �� �����, �������� ���
    {
        Debug.Log("Selected");
        SelectionCheck allyUnit = hitInfo.GetComponent<SelectionCheck>();
        if (allyUnit)
        {
            allyUnit.isSelected = true;
            storage.SelectedUnits.Add(allyUnit.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision) //���� ����
    {
        SelectionCheck allyUnit = collision.GetComponent<SelectionCheck>();
        if (allyUnit && !IsDone)
        {
            allyUnit.isSelected = false;
            storage.SelectedUnits.Remove(allyUnit.gameObject);
        }
        Debug.Log("DeSelected");
    }
    private void DeleteSelected() //�������� ������ ���������� ������ � ���������
    {
        while (storage.SelectedUnits.Count != 0)
        {
            if (storage.SelectedUnits.First() != null)
                storage.SelectedUnits.First().GetComponent<SelectionCheck>().isSelected = false;
            storage.SelectedUnits.RemoveAt(0);
        }
    }
}
