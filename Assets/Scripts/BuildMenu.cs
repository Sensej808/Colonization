using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//скрипт меню дрона
public class BuildMenu : MonoBehaviour
{
    public bool buildMenuIsOpen = false; //true - если меню строительства открыто
    public SelectionCheck Selection;
    public Build builder;
    public AllyMoving Moving;
    private bool doStruct = false;//идёт строительство
    private string buildButton; //имя кнопки которая отвечает за строительство, нажатое последним
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));//массив имён кнопок
    //переменные зданий
    public GameObject structR;
    public GameObject flyStructR;
    public GameObject structQ;
    public GameObject flyStructQ;
    void Update()
    {
        if (Input.GetKey("b") && Selection.isSelected) //открываем меню строительства
            buildMenuIsOpen = true;
        if (!Selection.isSelected) //заккрываем меню строительства
            buildMenuIsOpen = false;
        ChoooseStruct();
    }
    void CreateStruct(string buttonName, GameObject flyStruct, GameObject Struct)
    {
        if (buildMenuIsOpen) //если меню открыто
        {
            if (Input.GetKey(buttonName)) //если нажимаем, создаётся шаблон здания
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
                if (builder.flyStruct.tag == "IsFreePosition") //если место свободно, дрон идёт к месту
                {
                    Moving.finalPos = builder.buildPos;
                    Moving.isMoving = true;
                }
                else //иначе билдер перестаёт работать
                {
                    builder.BuildStruct(Struct);
                    builder.structBe = false;
                }
                if (transform.position == builder.buildPos) //если позиция дрона равна позиции шаблона здания, то оно строится
                {
                    builder.BuildStruct(Struct);
                    builder.structBe = false;
                    Moving.isMoving = false;
                    doStruct = false;
                }
                if ((Input.GetMouseButtonDown(1) || Input.GetKey("s")) && Selection.isSelected) //если мы передумали строить и остановились
                {                                                    //или отправили дрона в другое место, то всё перестаёт работать
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
