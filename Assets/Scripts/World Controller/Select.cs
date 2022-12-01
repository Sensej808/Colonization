using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TODO: ������� ��������� ��������� ������ �������� �� ������
//������ ��� ������ ����� ���������
public class Select : MonoBehaviour
{
    //��� ���������� ��������� ����� ��������� ��������� � �������� OnTriggerExit2D ��� ���� ��������.
    //����� ��� ���������� ������� ��������, ��������� IsDone, ������� ����� true ��� ��������
    public bool IsDone = false; 
    public CommandController controller; //��� ���������� ����� ����������� � selected units
    public List<GameObject> SelectedUnits;
    private void Start()
    {
        DeleteSelected();
        controller = GameObject.Find("UnitCommandController").GetComponent<CommandController>();
    }
    private void OnTriggerEnter2D(Collider2D hitInfo) //���� ����� ������� �� �����, �������� ���
    {
        SelectionCheck allyUnit = hitInfo.GetComponent<SelectionCheck>();
        if (allyUnit)
        {
            //allyUnit.isSelected = true;
            SelectedUnits.Add(allyUnit.gameObject);
            Debug.Log("Selected");

        }
    }
    private void OnTriggerExit2D(Collider2D collision) //���� ����
    {
        SelectionCheck allyUnit = collision.GetComponent<SelectionCheck>();
        if (allyUnit && !IsDone)
        {
            //allyUnit.isSelected = false;
            SelectedUnits.Remove(allyUnit.gameObject);
            Debug.Log("DeSelected");
        }
    }
    private void DeleteSelected() //�������� ������ ���������� ������ � ���������
    {
        while(SelectedUnits.Count != 0)
        {
            //SelectedUnits.First().GetComponent<SelectionCheck>().isSelected = false;
            SelectedUnits.RemoveAt(0);

        }
    }

    //��� ��������� � ������ � ������, ������ �� ���� ��������
    private void RemoveStruct()
    {
        foreach (var item in SelectedUnits)
        {
            if (item.GetComponent<BaseStructClass>())
            {
                SelectedUnits.Remove(item);
            }
        }
    }
    private void OnDestroy()
    {
        controller.UpdateSelection(SelectedUnits);
    }
}