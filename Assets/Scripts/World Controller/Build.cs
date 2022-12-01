using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public GameObject building; //ѕеременна€ здани€
    public Vector3 pos; //координаты строительства
    public GameObject buildingUnderConstruction;
    //¬озвращает, свободно ли место строительства
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
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        buildingUnderConstruction = Instantiate(building, pos, building.transform.rotation);
    }
    //идЄт строить здание
    public void GoBuild()
    {
        unit.Moving.finalPos = pos;
        unit.Moving.isMoving = true;
        unit.state = StateUnit.GoUseAbility;
    }
    //объедин€ет предыдущие функции
    //если здание выбрано, то есть не пустое
    // то смотрим, свободна ли площадка
    //если да, идЄм строить, иначе заканчиваем всЄ, обнул€€ переменную здани€
    //если дошли, и там до сих пор свободна территори€, то строим, здание обнул€ем, заканчиваем
    public void GoAndBuild()
    {
        if (building != null)
        {
            if (isFreePosition())
                GoBuild();
            else
            {
                unit.state = StateUnit.Normal;
                building = null;
                unit.Moving.isMoving = false;
            }
            if (transform.position == pos && isFreePosition())
            {
                SetFrame();
                building = null;
                unit.state = StateUnit.BuildStruct;
            }
        }
    }
    public void BuildStruct()
    {
        if (unit.state == StateUnit.BuildStruct)
        {
            if (buildingUnderConstruction.GetComponent<Frame>().constructionTime <= 0)
            {
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
        //мы всегда идЄм строить, но всЄ работает только если building не пустой, иначе всЄ сразу обрываетс€
        GoAndBuild();
        BuildStruct();
    }
    //это нужно дл€ того, чтобы если мы пошли строить а там было свободно, а пришли и зан€то, строителть не билс€
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
