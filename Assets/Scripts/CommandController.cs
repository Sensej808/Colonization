using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public List<GameObject> selectedUnits; //����, ���������� ���������� ������
    public bool clickInterface; //���������� ����������, ����� ��� ����� ����������� �������: �� ����������, ��� ����������
    public Dictionary<string, bool> KeyOnMenu; //������ ������ ����������, ���������, ������� ������ value true, ���� ������ ������
    public int k;

    //�������� ���� ���������� ������
    public void UpdateSelection(List<GameObject> SelectedUnits)
    {
        foreach (var unit in selectedUnits)
        {
            unit.GetComponent<SelectionCheck>().isSelected = false;
            unit.GetComponent<SelectionCheck>().Demonstrate();
        }
        selectedUnits = null;
        selectedUnits = SelectedUnits;
        foreach (var unit in SelectedUnits)
        {
            unit.GetComponent<SelectionCheck>().isSelected = true;
            unit.GetComponent<SelectionCheck>().Demonstrate();
        }
    }
    
    //�������� ���������� ����� �� ������(�� �� ���� ����������), �� ��������� � �����-�� ����������
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            if ((go.transform.position - pos).magnitude < min && go.GetComponent<BaseUnitClass>().state == StateUnit.Normal)
            {
                min = (go.transform.position - pos).magnitude;
                nearest = go;
            }
        }
        return nearest;
    }
    //��������� ��� ������ ����������
    //(��� �����, ������ ��� � 1 ������ ����� ������ ����� 1 �������)
    public void AllButtonFalse()
    {
        List<string> str = new List<string>();
        foreach (KeyValuePair<string, bool> button in KeyOnMenu)
        {
            str.Add(button.Key);
        }
        foreach (string s in str)
            KeyOnMenu[s] = false;
    }
    //��� �� ������� ��� ������ �������, ������� �������, ����� ������ ��� ������
    //(��������� ��� ����� ������, �� ������ ��� �������������)

    //�������� ������  � ����������, �������, ��� ������ ������ ����������
    public void Click(string key)
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu[key] = true;
    }
    //������� ������, ������������� ������� � ������, ���������� ��������, ��������� ���������� ���������� ������ ������
    public void SetStruct(string nameStruct)
    {
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos.z = 0;
        List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        GameObject builder = Nearest(myPos, group);
        if (builder != null)
            builder.GetComponent<Build>().SetStructPos(Resources.Load<GameObject>(nameStruct), myPos);
    }
    //������������� ������������� ���� ���������� �������
    public void StopBuild()
    {
            List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
            foreach (GameObject go in group)
            {
                go.GetComponent<Build>().StopBuild();
            }
    }
    public void Start()
    {
        KeyOnMenu = new Dictionary<string, bool>();
        k = 100;
        KeyOnMenu.Add("Q", false); //��������� ������ Q � ���������
        KeyOnMenu.Add("R", false); //��������� ������ R � ���������
    }
    void Update()
    {
        //���� �� ������ ������ � ����������, �� ��������� ������� �� ����������
        if (!clickInterface)
        {
            //���� ��������� ������, ����� ������� ������
            if (Input.GetKeyUp(KeyCode.Q))
                SetStruct("Prefabs/FrameQ");
            if (Input.GetKeyUp(KeyCode.R))
                SetStruct("Prefabs/FrameR");
        }
        //���� ������ ������ � �����������, �� ��������� ������� �� ����� ����
        if (clickInterface)
        {
            //���� ������ ���, �� ����� ������� ���������������� ������
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["R"])
            {
                SetStruct("Prefabs/FrameR");
                k = 100;
                AllButtonFalse();
            }
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["Q"])
            {
                SetStruct("Prefabs/FrameQ");
                k = 100;
                AllButtonFalse();
            }
        }
        //���� ��� ESC ��� S(�� ���� stop), �� ���������� ��� ������� � ���������� ������
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            clickInterface = false;
            StopBuild();
        }
        //��������
        //������ ���, ����� �� ��� ��� �� �����, �� ������� ����� ������
        //� ��� �� ������� 5 �������, ����� �� ������� ���� ��������� ���������, ����� ������ ����������(�������, �� ���� ������ ����� �����)
        //������ �������� �������, � ��� ��� ��� ���������� �� ���, �� ����� �������� ����� ���������
        //� ��� ��� � ��� ������ ����� �� ����� ����� 5 �������, �� ��� ������ �������������
        //������� ��������� ����� ��������� �����, �� ���������, ������� �� ������� ���-�� ��� ���
        //������ �� ������ ����� �� �������� clickInterface
        //������ ��� ������� ���, ��� �� �����������, � ����� ���� ������� ���������� ��������� �����, ������� ����� �������
        //������� ����� �������� ���� �� ���������� ����� ��������� ����� �������� �����
        if (k != 0)
        {
            k--;
            if (k == 0)
            {
                clickInterface = false;
            }
        }
    }
}
