using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public Vector3 buildPos; //место строительства
    public bool structBe = false; //создан ли шаблон здания
    public GameObject flyStruct; //шаблон здания
    public void SelectBuildPosition(GameObject flyBuildStruct) //с помощью него выбираем место строительства
    {
        buildPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        buildPos.x = Mathf.RoundToInt(buildPos.x); //округляем до целых, чтобы здания нельзя было строить где попало
        buildPos.y = Mathf.RoundToInt(buildPos.y); //округляем до целых, чтобы здания нельзя было строить где попало
        buildPos.z = 0;
        if (!structBe) //если шаблона здания нет, то создаём его
        {
            flyStruct = Instantiate(flyBuildStruct, buildPos, flyBuildStruct.transform.rotation);
            structBe = true;
        }
        else //если он создан, то передвигаем его
        {
            if (flyStruct != null)
                flyStruct.transform.position = new Vector3(buildPos.x, buildPos.y, buildPos.z);
        }
    }
    public void BuildStruct(GameObject BuildStruct) //с помощью уже строим здание
    {
        if (flyStruct.tag == "IsFreePosition" && flyStruct != null) //если место свободно, то строим здание
            Instantiate(BuildStruct, buildPos, flyStruct.transform.rotation);
            Destroy(flyStruct); //удаляем шаблон
            structBe = false;
    }
}
