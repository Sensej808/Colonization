using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ObjectChangeEventStream;
//using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public GameObject building; //Переменная здания
    public Vector3 pos; //координаты строительства
    public GameObject buildingUnderConstruction;
    //Возвращает, свободно ли место строительства
    public bool isFreePosition()
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        GameObject territory = building.transform.Find("MyTerritory").gameObject;
        Collider2D[] strangersObjects = Physics2D.OverlapBoxAll(pos, territory.transform.localScale, 0);
        List<Collider2D> listTerritory = new List<Collider2D>();
        foreach (Collider2D go in strangersObjects)
        {
            if (go.gameObject.name == "MyTerritory")
                listTerritory.Add(go);
        }
        return (listTerritory.Count == 0);
    }
    //строит здание
    public void SetFrame()
    {
        Debug.Log("Create a frame");
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        buildingUnderConstruction = Instantiate(building, pos, building.transform.rotation);
    }
    //идёт строить здание
    public void GoBuild()
    {
        unit.Moving.MoveTo(pos);
        unit.Moving.onMovingEnd += GoAndBuild; //Подписываемся на событие:Когда юнит дойдет, начнется строительство
        unit.state = StateUnit.GoUseAbility;
    }
    //объединяет предыдущие функции
    //если здание выбрано, то есть не пустое
    // то смотрим, свободна ли площадка
    //если да, идём строить, иначе заканчиваем всё, обнуляя переменную здания
    //если дошли, и там до сих пор свободна территория, то строим, здание обнуляем, заканчиваем
    public void GoAndBuild()
    {
        if (building != null)
        {
            if (isFreePosition())
            {
                Debug.Log(transform.position - new Vector3(0.5F, 0.5F, 0));
                if (transform.position - new Vector3(0.5F, 0.5F, 0) == pos)
                {
                    unit.Moving.onMovingEnd -= GoAndBuild;
                    Debug.Log("Builder arrived");
                    SetFrame();
                    building = null;
                    unit.state = StateUnit.BuildStruct;
                    BuildStruct();
                }
                else
                {
                Debug.Log("Go to cons pos");
                GoBuild();
                }
            }

            else
            {
                unit.state = StateUnit.Normal;
                building = null;
                unit.Moving.isMoving = false;
            }
            
        }
    }
    public void BuildStruct()
    {
        if (unit.state == StateUnit.BuildStruct)
        {
            if (buildingUnderConstruction.GetComponent<Frame>().time <= 0)
            {
                Debug.Log("building struct...");
                Instantiate(buildingUnderConstruction.GetComponent<Frame>().futureBuilding, buildingUnderConstruction.transform.position, buildingUnderConstruction.transform.rotation);
                Destroy(buildingUnderConstruction);
                unit.state = StateUnit.Normal;
                StopBuild();
            }
        }

    }
    //устанавливаем значение зданию, которое будем строить, и координаты, где будем строить
    public void SetStructPos(GameObject myBuilding, Vector3 myPos)
    {
        building = myBuilding;
        pos = myPos;
    }
    //прекращаем строительство
    public void StopBuild()
    {
        unit.Moving.isMoving = false;
        building = null;
        unit.state = StateUnit.Normal;
        if (buildingUnderConstruction != null)
            Destroy(buildingUnderConstruction);
    }
    public void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
    }
    public void Update()
    {
        //мы всегда идём строить, но всё работает только если building не пустой, иначе всё сразу обрывается(Плохая идея)
        //GoAndBuild();
        BuildStruct();
    }
    //это нужно для того, чтобы если мы пошли строить а там было свободно, а пришли и занято, строителть не бился
    //головой о здание, а сразу сделал пустым building и стал адыхать
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform territory = collision.gameObject.transform.Find("MyTerritory");
        if (territory != null && collision.OverlapPoint(pos) && buildingUnderConstruction != null)
        {
            if (territory != buildingUnderConstruction.transform.Find("MyTerritory"))
                StopBuild();
        }
    }
}
