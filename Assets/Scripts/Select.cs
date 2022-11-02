using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//скрипт для работы сетки выделения
public class Select : MonoBehaviour
{
    public bool k = false; //не помню зачем но без этого у меня не работало
    private GameObject[] arrAlliedUnits; //массив союзных юнитов
    private void Start()
    {
        DeleteSelected();
    }
    private void OnTriggerEnter2D(Collider2D hitInfo) //если сетка доходит до юнита, выделяет его
    {
        SelectionCheck allyUnit = hitInfo.GetComponent<SelectionCheck>();
        if (allyUnit)
        {
            allyUnit.isSelected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //если сетка выходит из юнита, удаляет его из выделения
    {
        SelectionCheck allyUnit = collision.GetComponent<SelectionCheck>();
        if (allyUnit && k)
            allyUnit.isSelected = false;
    }
    private void DeleteSelected() //ставит isSelected = false всем юнитам, при создании новой сетки
    {
        arrAlliedUnits = GameObject.FindGameObjectsWithTag("Allied");
        foreach (GameObject en in arrAlliedUnits)
        {
            if (en != null)
            {
                SelectionCheck Selection = en.GetComponent<SelectionCheck>();
                Selection.isSelected = false;
            }
        }
    }
}
