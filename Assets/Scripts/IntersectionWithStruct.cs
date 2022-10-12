using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionWithStruct : MonoBehaviour
{
    private int i = 0;//нужно для корректного взаимодействия шаблона здания с уже построенными
    public void OnTriggerExit2D(Collider2D collision)
    {
        //нужно для корректного взаимодействия шаблона здания с уже построенными
        DoUnits struction = collision.GetComponent<DoUnits>();
        if (struction)
        {
            i--;
        }
        if (i == 0)
        {
            gameObject.tag = "IsFreePosition";//тэг говорит о том что место свободно
            //меняем цвет
            gameObject.transform.Find("Body").gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        DoUnits struction = hitInfo.GetComponent<DoUnits>();
        if (struction)
        {
            i++;
            gameObject.tag = "IsNotFreePosition";//тэг говорит о том что место занятно
            //меняем цвет
            gameObject.transform.Find("Body").gameObject.GetComponent<Renderer>().material.color = new Vector4(251 / 255.0f, 15 / 255.0f, 15 / 255.0f, 1);
        }
    }
}
