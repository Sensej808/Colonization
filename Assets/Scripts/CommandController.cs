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
    public void GetSelectedUnits()
    {
        selectedUnits.Clear();
        GameObject[] arrSelectedUnits = GameObject.FindGameObjectsWithTag("Allied");
        foreach (GameObject go in arrSelectedUnits)
        {
            if (go.GetComponent<SelectionCheck>().isSelected)
                selectedUnits.Add(go);
        }
    }
    //�������� ���������� ����� �� ������(�� �� ���� ����������), �� ��������� � �����-�� ����������
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            if ((go.transform.position - pos).magnitude < min)
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

    //�������� ������ R � ����������, �������, ��� ������ ������ ����������
    public void ClickR()
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu["R"] = true;
    }
    //�������� ������ Q � ����������, �������, ��� ������ ������ ����������
    public void ClickQ()
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu["Q"] = true;
    }
    //��� ����� �������, ���������� �� ����� ������ ��� �������������

    //������� ������, ������������� ������� � ������, ���������� ��������, ��������� ���������� ���������� ������ ������
    public void SetStruct(string nameStruct)
    {
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos.z = 0;
        GetSelectedUnits();
        List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        GameObject builder = Nearest(myPos, group);
        if (builder != null)
            builder.GetComponent<Build>().SetStructPos(Resources.Load<GameObject>(nameStruct), myPos);
    }
    //����� ����� ��� ������ � ����������, ������ ��� ��� �� ��������� ������� � ����������� :(
    public void SetStructR() { SetStruct("Prefabs/StructR"); }
    public void SetStructQ() { SetStruct("Prefabs/StructQ"); }
    //������������� ������������� ���� ���������� �������
    public void StopBuild()
    {
            GetSelectedUnits();
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
                SetStructQ();
            if (Input.GetKeyUp(KeyCode.R))
                SetStructR();
        }
        //���� ������ ������ � �����������, �� ��������� ������� �� ����� ����
        if (clickInterface)
        {
            //���� ������ ���, �� ����� ������� ���������������� ������
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["R"])
            {
                SetStructR();
                k = 100;
            }
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["Q"])
            {
                SetStructQ();
                k = 100;
            }
        }
        //���� ��� ESC ��� S(�� ���� stop), �� ���������� ��� ������� � ���������� ������
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            clickInterface = false;
            StopBuild();
        }
        //��������
        //������ ����� ���, ����� �� ��� ��� �� �����, �� ������� ����� ������
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
                clickInterface = false;
        }
    }
}
