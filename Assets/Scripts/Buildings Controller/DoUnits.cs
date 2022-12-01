using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Написать скрипт спауна юнитов(будет компонентом здания, скорее всего, значит должен реагировать на команды здания)

//Скрипт создания юнитов от зданий
public class DoUnits : MonoBehaviour
{
    public double realTimeSpawning;
    public GameObject myUnit;
    public BaseStructClass myStruct;
    public void Start()
    {
        myStruct = gameObject.GetComponent<BaseStructClass>();
    }
    public void SetUnit(GameObject unit, double timeSpawning)
    {
        realTimeSpawning = timeSpawning;
        myUnit = unit;
    }
    public void SpawnUnit()
    {
        if (myUnit != null && realTimeSpawning >= 0)
        {
            realTimeSpawning -= 0.1f;
            if (realTimeSpawning <= 0)
                Instantiate(myUnit, gameObject.transform.Find("SpawnUnits").position, transform.rotation);
        }
        if (realTimeSpawning <= 0)
            myUnit = null;
    }
    void Update()
    {
        if (myStruct.Selection.isSelected && Input.GetKeyDown("q"))
            SetUnit(Resources.Load<GameObject>("Prefabs/Engineer"), 40);
        if (myStruct.Selection.isSelected && Input.GetKeyDown("c"))
            SetUnit(Resources.Load<GameObject>("Prefabs/Cyborg"), 40);
        if (myStruct.Selection.isSelected && Input.GetKeyDown(KeyCode.Escape))
            myUnit = null;
            SpawnUnit();
    }
}
