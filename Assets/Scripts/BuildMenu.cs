using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
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
    void Start()
    {
        structR = Resources.Load<GameObject>("Prefabs/StructR");
        flyStructR = Resources.Load<GameObject>("Prefabs/FlyStructR");
        structQ = Resources.Load<GameObject>("Prefabs/StructQ");
        flyStructQ = Resources.Load<GameObject>("Prefabs/FlyStructQ");
        Selection = gameObject.GetComponent<SelectionCheck>();
        builder = gameObject.GetComponent<Build>();
        Moving = gameObject.GetComponent<AllyMoving>();
    }
    void Update()
    {
        if (Input.GetKey("b") && Selection.isSelected) //открываем меню строительства
            buildMenuIsOpen = true;
        if (!Selection.isSelected) //заккрываем меню строительства
            buildMenuIsOpen = false;
        ChoooseStruct();
    }
    void CreateStruct(string buttonName, GameObject flyStruct, GameObject Struct)//функция которая полностью отвечает за строительство
    {
        if (buildMenuIsOpen) //если меню открыто
        {
            if (Input.GetKey(buttonName)) //если нажимаем, создаётся шаблон здания
                builder.SelectBuildPosition(flyStruct);
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
                    builder.BuildStruct(Struct);
                if (transform.position == builder.buildPos) //если позиция дрона равна позиции шаблона здания, то оно строится
                {
                    builder.BuildStruct(Struct);
                    Moving.isMoving = false;
                    doStruct = false;
                }
                if ((Input.GetMouseButtonDown(1) || Input.GetKey("s")) && Selection.isSelected) //если мы передумали строить и остановились
                {                                                    //или отправили дрона в другое место, то всё перестаёт работать
                    Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Moving.finalPos.z = 0;
                    builder.flyStruct.tag = "IsNotFreePosition";
                    builder.BuildStruct(Struct);
                }
            }
            //если мы пока идёт рабочий захотели сделать здание в другом месте, то он останавливается
            if (Input.GetKey(buttonName) && Selection.isSelected)
                Moving.isMoving = false;
        }
    }
    void ChoooseStruct()//функция выбора ЧТО строить
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Selection.isSelected && 
                    Input.GetKey(keyCode) && keyCode.ToString() != "Mouse1" && keyCode.ToString() != "Mouse0"
                    && keyCode.ToString() != "B")
                {
                        builder.structBe = false;
                        Destroy(builder.flyStruct);
                        Moving.isMoving = false;
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
