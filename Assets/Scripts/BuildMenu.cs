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
    //переменные зданий
    public GameObject structR;
    public GameObject flyStructR;
    void Update()
    {
        if (Input.GetKey("b") && Selection.isSelected) //открываем меню строительства
            buildMenuIsOpen = true;
        if (!Selection.isSelected) //заккрываем меню строительства
            buildMenuIsOpen = false;
        if(buildMenuIsOpen) //если меню открыто
        {
            if(Input.GetKey("r")) //если нажимаем r, создаётся шаблон здания
            {
                builder.SelectBuildPosition(flyStructR);
                builder.structBe = true;
            }
            if(!Input.GetKey("r") && builder.structBe)
            {
                if (builder.flyStruct.tag == "IsFreePosition") //если место свободно, дрон идёт к месту
                {
                    Moving.finalPos = builder.buildPos;
                    Moving.isMoving = true;
                }
                else //иначе билдер перестаёт работать
                {
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                }
                if (transform.position == builder.buildPos) //если позиция дрона равна позиции шаблона здания, то оно строится
                {
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                    Moving.isMoving = false;
                }
                if(Input.GetMouseButtonDown(1) || Input.GetKey("s")) //если мы передумали строить и остановились
                {                                                    //или отправили дрона в другое место, то всё перестаёт работать
                    builder.flyStruct.tag = "IsNotFreePosition";
                    builder.BuildStruct(structR);
                    builder.structBe = false;
                }
            }
        }
    }
}
