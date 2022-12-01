using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TODO: Сделать возможным выделение одного элемента по щелчку
//TODO: Организовать выделение юнитов только после завершения выделения(удаления сетки), а не сразу, когда, сетка существует
//скрипт для работы сетки выделения
public class Select : MonoBehaviour
{
    //При завершении выделения Сетка выделения удаляется и вызывает OnTriggerExit2D для всех объектов.
    //Чтобы все выделенные объекты остались, добавлена IsDone, которая равна true при удалении
    public bool IsDone = false; 
    public InGameStorage storage;
    private void Start()
    {
        storage = GameObject.Find("Storage").GetComponent<InGameStorage>();
        DeleteSelected();
    }
    private void OnTriggerEnter2D(Collider2D hitInfo) //если сетка доходит до юнита, выделяет его
    {
        Debug.Log("Selected");
        SelectionCheck allyUnit = hitInfo.GetComponent<SelectionCheck>();
        if (allyUnit)
        {
            allyUnit.isSelected = true;
            storage.SelectedUnits.Add(allyUnit.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Если юнит
    {
        SelectionCheck allyUnit = collision.GetComponent<SelectionCheck>();
        if (allyUnit && !IsDone)
        {
            allyUnit.isSelected = false;
            storage.SelectedUnits.Remove(allyUnit.gameObject);
        }
        Debug.Log("DeSelected");
    }
    private void DeleteSelected() //отчищает список выделенных юнитов в Хранилище
    {
        while (storage.SelectedUnits.Count != 0)
        {
            if (storage.SelectedUnits.First() != null)
                storage.SelectedUnits.First().GetComponent<SelectionCheck>().isSelected = false;
            storage.SelectedUnits.RemoveAt(0);
        }
    }
}
