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
    private bool doStruct = false;
    //���������� ������
    public GameObject structR;
    public GameObject flyStructR;
    void Update()
    {
        if (Input.GetKey("b") && Selection.isSelected) //��������� ���� �������������
            buildMenuIsOpen = true;
        if (!Selection.isSelected) //���������� ���� �������������
            buildMenuIsOpen = false;
        if (buildMenuIsOpen) //���� ���� �������
        {
            if (Input.GetKey("r")) //���� �������� r, �������� ������ ������
            {
                builder.SelectBuildPosition(flyStructR);
                builder.structBe = true;
            }
            doStruct = true;
        }
        if (doStruct)
        {
            if(!Input.GetKey("r") && builder.structBe)
            {
                if (builder.flyStruct.tag == "IsFreePosition") //���� ����� ��������, ���� ��� � �����
                {
                    Moving.finalPos = builder.buildPos;
                    Moving.isMoving = true;
                }
                else //����� ������ �������� ��������
                {
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                }
                if (transform.position == builder.buildPos) //���� ������� ����� ����� ������� ������� ������, �� ��� ��������
                {
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                    Moving.isMoving = false;
                    doStruct = false;
                }
                if((Input.GetMouseButtonDown(1) || Input.GetKey("s")) && Selection.isSelected) //���� �� ���������� ������� � ������������
                {                                                    //��� ��������� ����� � ������ �����, �� �� �������� ��������
                    Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Moving.finalPos.z = 0;
                    builder.flyStruct.tag = "IsNotFreePosition";
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                }
            }
        }
    }
}
