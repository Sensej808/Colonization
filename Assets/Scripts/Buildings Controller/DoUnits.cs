using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Переделать таймер
//Скрипт создания юнитов от зданий
public class DoUnits : MonoBehaviour
{
    public float realTimeSpawning;
    public BaseStructClass myStruct;
    public Queue<GameObject> queueUnits;
    public GameObject unit;
    public void Start()
    {
        myStruct = gameObject.GetComponent<BaseStructClass>();
        queueUnits = new Queue<GameObject>();
    }
    public void AddUnit(GameObject unit)
    {
        queueUnits.Enqueue(unit);
    }
    public void CreateUnit()
    {
        if (queueUnits.Count != 0 && unit == null)
        {
            unit = queueUnits.Peek();
            realTimeSpawning = unit.GetComponent<BaseUnitClass>().ProductionTime;
            print(realTimeSpawning);
        }
        if (unit != null)
        {
            realTimeSpawning -= 0.1f;
            if (realTimeSpawning <= 0)
            {
                Instantiate(unit, gameObject.transform.Find("SpawnUnits").position, transform.rotation);
                unit = null;
                print("Completed");
                queueUnits.Dequeue();
            }
        }
    }
    public void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.W) && myStruct.Selection.isSelected)
            AddUnit(Resources.Load<GameObject>("Prefabs/Cyborg"));
        */
        CreateUnit();
    }
}
