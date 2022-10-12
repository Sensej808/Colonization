using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ���� �����
public class BuildMenu : MonoBehaviour
{
    public bool buildMenuIsOpen = false; //true - ���� ���� ������������� �������
    public SelectionCheck Selection;
    public Build builder;
    public AllyMoving Moving;
    private bool doStruct = false;//��� �������������
    private string buildButton; //��� ������ ������� �������� �� �������������, ������� ���������
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));//������ ��� ������
    //���������� ������
    public GameObject structR;
    public GameObject flyStructR;
    public GameObject structQ;
    public GameObject flyStructQ;
    void Update()
    {
        if (Input.GetKey("b") && Selection.isSelected) //��������� ���� �������������
            buildMenuIsOpen = true;
        if (!Selection.isSelected) //���������� ���� �������������
            buildMenuIsOpen = false;
        ChoooseStruct();
    }
    void CreateStruct(string buttonName, GameObject flyStruct, GameObject Struct)
    {
        if (buildMenuIsOpen) //���� ���� �������
        {
            if (Input.GetKey(buttonName)) //���� ��������, �������� ������ ������
            {
                builder.SelectBuildPosition(flyStruct);
                builder.structBe = true;
            }
            doStruct = true;
        }
        if (doStruct)
        {
            if (!Input.GetKey(buttonName) && builder.structBe)
            {
                if (builder.flyStruct.tag == "IsFreePosition") //���� ����� ��������, ���� ��� � �����
                {
                    Moving.finalPos = builder.buildPos;
                    Moving.isMoving = true;
                }
                else //����� ������ �������� ��������
                {
                    builder.BuildStruct(Struct);
                    builder.structBe = false;
                }
                if (transform.position == builder.buildPos) //���� ������� ����� ����� ������� ������� ������, �� ��� ��������
                {
                    builder.BuildStruct(Struct);
                    builder.structBe = false;
                    Moving.isMoving = false;
                    doStruct = false;
                }
                if ((Input.GetMouseButtonDown(1) || Input.GetKey("s")) && Selection.isSelected) //���� �� ���������� ������� � ������������
                {                                                    //��� ��������� ����� � ������ �����, �� �� �������� ��������
                    Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Moving.finalPos.z = 0;
                    builder.flyStruct.tag = "IsNotFreePosition";
                    builder.BuildStruct(Struct);
                    builder.structBe = false;
                }
            }
        }
    }
    void ChoooseStruct()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode) && keyCode.ToString() != "Mouse1" && keyCode.ToString() != "Mouse0"
                    && keyCode.ToString() != "B")
                {
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
