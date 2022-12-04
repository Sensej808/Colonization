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
    //public CommandController controller; //��� ���������� ����� ����������� � selected units
    public List<GameObject> SelectedUnits;
    private void Start()
    {
        DeleteSelected();
        //controller = GameObject.Find("UnitCommandController").GetComponent<CommandController>();
    }
    private void OnTriggerEnter2D(Collider2D hitInfo) //���� ����� ������� �� �����, �������� ���
    {
        SelectionCheck allyUnit = hitInfo.GetComponent<SelectionCheck>();
        if (allyUnit)
        {
            if (allyUnit.gameObject.tag == "Allied")
            {
                //allyUnit.isSelected = true;
                SelectedUnits.Add(allyUnit.gameObject);
                Debug.Log("Selected");
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision) //���� ����
    {
        SelectionCheck allyUnit = collision.GetComponent<SelectionCheck>();
        if (allyUnit && !IsDone)
        {
            if (allyUnit.gameObject.tag == "Allied")
            {
                //allyUnit.isSelected = false;
                SelectedUnits.Remove(allyUnit.gameObject);
                Debug.Log("DeSelected");
            }
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
        Collider2D[] gos = Physics2D.OverlapBoxAll(transform.position, new Vector3(0.001f, 0.001f, 0.001f), 0);
        foreach (Collider2D go in gos)
        {
            if (go.gameObject.GetComponent<SelectionCheck>() != null)
            {
                if (go.gameObject.tag == "Allied")
                    SelectedUnits.Add(go.gameObject);
            }
        }
        //controller.UpdateSelection(SelectedUnits);
        if (SelectedUnits.Find(x => x.GetComponent<BaseStructClass>() != null) && SelectedUnits.Find(x => x.GetComponent<BaseStructClass>() == null))
            SelectedUnits.RemoveAll(x => x.GetComponent<BaseStructClass>() != null);
        foreach (SelectionCheck sc in GameObject.FindObjectsOfType<SelectionCheck>())
        {
            sc.isSelected = false;
            sc.Demonstrate();
        }
        foreach (GameObject go in SelectedUnits)
        {
            go.GetComponent<SelectionCheck>().isSelected = true;
            go.GetComponent<SelectionCheck>().Demonstrate();
        }
    }
}
