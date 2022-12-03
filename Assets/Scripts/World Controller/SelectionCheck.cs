using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс, который хранит переменную для выделения юнитов
public class SelectionCheck : MonoBehaviour
{
    public bool isSelected = false;
    private Rect rect;
    Sprite selectBox;


    //Отрисовка выделения объекта
    public void Demonstrate()
    {
        //Debug.Log(isSelected);
        transform.GetChild(0).gameObject.SetActive(isSelected);
    }
}
