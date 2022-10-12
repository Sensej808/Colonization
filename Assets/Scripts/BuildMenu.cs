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
    //���������� ������
    public GameObject structR;
    public GameObject flyStructR;
    void Update()
    {
        if (Input.GetKey("b") && Selection.isSelected) //��������� ���� �������������
            buildMenuIsOpen = true;
        if (!Selection.isSelected) //���������� ���� �������������
            buildMenuIsOpen = false;
        if(buildMenuIsOpen) //���� ���� �������
        {
            if(Input.GetKey("r")) //���� �������� r, �������� ������ ������
            {
                builder.SelectBuildPosition(flyStructR);
                builder.structBe = true;
            }
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
                }
                if(Input.GetMouseButtonDown(1) || Input.GetKey("s")) //���� �� ���������� ������� � ������������
                {                                                    //��� ��������� ����� � ������ �����, �� �� �������� ��������
                    builder.flyStruct.tag = "IsNotFreePosition";
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                }
            }
        }
    }
}
