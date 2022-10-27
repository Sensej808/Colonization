using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
//������ ���� �����
public class BuildMenu : MonoBehaviour
{
    public bool buildMenuIsOpen = false; //true - ���� ���� ������������� �������
    private bool doStruct = false;//��� �������������
    private string buildButton; //��� ������ ������� �������� �� �������������, ������� ���������
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));//������ ��� ������
    public EngineerClass unit;
    //���������� ������
    public GameObject structR;
    public GameObject flyStructR;
    public GameObject structQ;
    public GameObject flyStructQ;
    void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
        structR = Resources.Load<GameObject>("Prefabs/StructR");
        flyStructR = Resources.Load<GameObject>("Prefabs/FlyStructR");
        structQ = Resources.Load<GameObject>("Prefabs/StructQ");
        flyStructQ = Resources.Load<GameObject>("Prefabs/FlyStructQ");
    }
    void Update()
    {
        if (Input.GetKey("b") && unit.Selection.isSelected) //��������� ���� �������������
            buildMenuIsOpen = true;
        if (!unit.Selection.isSelected) //���������� ���� �������������
            buildMenuIsOpen = false;
        ChoooseStruct();
    }
    void CreateStruct(string buttonName, GameObject flyStruct, GameObject Struct)//������� ������� ��������� �������� �� �������������
    {
        if (buildMenuIsOpen) //���� ���� �������
        {
            if (Input.GetKey(buttonName)) //���� ��������, �������� ������ ������
                unit.Builder.SelectBuildPosition(flyStruct);
            doStruct = true;
        }
        if (doStruct)
        {
            if (!Input.GetKey(buttonName) && unit.Builder.structBe)
            {
                if (unit.Builder.flyStruct.tag == "IsFreePosition") //���� ����� ��������, ���� ��� � �����
                {
                    unit.Moving.finalPos = unit.Builder.buildPos;
                    unit.Moving.isMoving = true;
                }
                else //����� ������ �������� ��������
                    unit.Builder.BuildStruct(Struct);
                if (transform.position == unit.Builder.buildPos) //���� ������� ����� ����� ������� ������� ������, �� ��� ��������
                {
                    unit.Builder.BuildStruct(Struct);
                    unit.Moving.isMoving = false;
                    doStruct = false;
                }
                if ((Input.GetMouseButtonDown(1) || Input.GetKey("s")) && unit.Selection.isSelected) //���� �� ���������� ������� � ������������
                {                                                    //��� ��������� ����� � ������ �����, �� �� �������� ��������
                    unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    unit.Moving.finalPos.z = 0;
                    unit.Builder.flyStruct.tag = "IsNotFreePosition";
                    unit.Builder.BuildStruct(Struct);
                }
            }
            //���� �� ���� ��� ������� �������� ������� ������ � ������ �����, �� �� ���������������
            if (Input.GetKey(buttonName) && unit.Selection.isSelected)
                unit.Moving.isMoving = false;
        }
    }
    void ChoooseStruct()//������� ������ ��� �������
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (unit.Selection.isSelected && 
                    Input.GetKey(keyCode) && keyCode.ToString() != "Mouse1" && keyCode.ToString() != "Mouse0"
                    && keyCode.ToString() != "B" && keyCode.ToString() != "A") 
                {
                        unit.Builder.structBe = false;
                        Destroy(unit.Builder.flyStruct);
                        unit.Moving.isMoving = false;
                        buildButton = keyCode.ToString();
                }
            }
        }
        if (buildButton == "Q")
            CreateStruct("q", flyStructQ, structQ);
        if (buildButton == "R")
        CreateStruct("r", flyStructR, structR);
    }
}
