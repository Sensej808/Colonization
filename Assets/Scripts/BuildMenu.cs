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
    private bool doStruct = false;//идёт строительство
    private string buildButton; //имя кнопки которая отвечает за строительство, нажатое последним
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));//массив имён кнопок
    public EngineerClass unit;
    //переменные зданий
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
        if (Input.GetKey("b") && unit.Selection.isSelected) //открываем меню строительства
            buildMenuIsOpen = true;
        if (!unit.Selection.isSelected) //заккрываем меню строительства
            buildMenuIsOpen = false;
        ChoooseStruct();
    }
    void CreateStruct(string buttonName, GameObject flyStruct, GameObject Struct)//функция которая полностью отвечает за строительство
    {
        if (buildMenuIsOpen) //если меню открыто
        {
            if (Input.GetKey(buttonName)) //если нажимаем, создаётся шаблон здания
                unit.Builder.SelectBuildPosition(flyStruct);
            doStruct = true;
        }
        if (doStruct)
        {
            if (!Input.GetKey(buttonName) && unit.Builder.structBe)
            {
                if (unit.Builder.flyStruct.tag == "IsFreePosition") //если место свободно, дрон идёт к месту
                {
                    unit.Moving.finalPos = unit.Builder.buildPos;
                    unit.Moving.isMoving = true;
                }
                else //иначе билдер перестаёт работать
                    unit.Builder.BuildStruct(Struct);
                if (transform.position == unit.Builder.buildPos) //если позиция дрона равна позиции шаблона здания, то оно строится
                {
                    unit.Builder.BuildStruct(Struct);
                    unit.Moving.isMoving = false;
                    doStruct = false;
                }
                if ((Input.GetMouseButtonDown(1) || Input.GetKey("s")) && unit.Selection.isSelected) //если мы передумали строить и остановились
                {                                                    //или отправили дрона в другое место, то всё перестаёт работать
                    unit.Moving.finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    unit.Moving.finalPos.z = 0;
                    unit.Builder.flyStruct.tag = "IsNotFreePosition";
                    unit.Builder.BuildStruct(Struct);
                }
            }
            //если мы пока идёт рабочий захотели сделать здание в другом месте, то он останавливается
            if (Input.GetKey(buttonName) && unit.Selection.isSelected)
                unit.Moving.isMoving = false;
        }
    }
    void ChoooseStruct()//функция выбора ЧТО строить
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
