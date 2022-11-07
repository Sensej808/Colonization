using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public GameObject building;
    public Vector3 pos;
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
    public void BuildStruct()
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        Instantiate(building, pos, building.transform.rotation);
    }

    public void GoBuild()
    {
        unit.Moving.finalPos = pos;
        unit.Moving.isMoving = true;
    }
    public void GoAndBuild()
    {
        if (building != null)
        {
            if (isFreePosition())
                GoBuild();
            else
                building = null;
            if (transform.position == pos && isFreePosition())
            {
                BuildStruct();
                building = null;
            }
        }
    }
    public void SetStructPos(GameObject myBuilding, Vector3 myPos)
    {
        building = myBuilding;
        pos = myPos;
    }

    public void StopBuild()
    {
        unit.Moving.isMoving = false;
        building = null;
    }
    public void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
    }
    public void SetStructQ(Vector3 myPos)
    {
        SetStructPos(Resources.Load<GameObject>("Prefabs/StructQ"), myPos);
    }
    public void SetStructR(Vector3 myPos)
    {
        SetStructPos(Resources.Load<GameObject>("Prefabs/StructR"), myPos);
    }

    public void Update()
    {
        GoAndBuild();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform territory = collision.gameObject.transform.Find("MyTerritory");
        if (territory != null && collision.OverlapPoint(pos))
        {
            unit.Moving.isMoving = false;
            building = null;
        }
    }
    /*
    public EngineerClass unit;
    public bool goBuild;
    public Vector3 pos;
    public GameObject myStruct;
    public bool banBuild;

    public bool doStructR;
    public bool doStructQ;
    public void BuildStruct(GameObject building)
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        GameObject territory = building.transform.Find("MyTerritory").gameObject;
        Collider2D[] strangersObjects = Physics2D.OverlapBoxAll(pos, territory.transform.localScale, 0);
        List<Collider2D> listTerritory = new List<Collider2D>();
        foreach(Collider2D go in strangersObjects)
        {
            if (go.gameObject.name == "MyTerritory")
                listTerritory.Add(go);
        }
        if (listTerritory.Count == 0)
            Instantiate(building, pos, building.transform.rotation);
    }
    public void GotoBuildPos()
    {
        unit.Moving.finalPos = pos;
        unit.Moving.isMoving = true;
    }
    public bool isFreePosition(GameObject building)
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
    public void FinalFunc(ref bool doStruct)
    {
        if (unit.Selection.isSelected && doStruct)
        {
            doStruct = false;
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            bool free = isFreePosition(myStruct);
            if (free)
            {
                GotoBuildPos();
                goBuild = true;
            }
        }
    }
    public void Choose()
    {
        FinalFunc(ref doStructR);
        FinalFunc(ref doStructQ);
    }
    public void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
        goBuild = false;
    }
    public void Update()
    {
        if (unit.Selection.isSelected)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Q)))
            {
                banBuild = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                goBuild = false;
                unit.Moving.isMoving = false;
            }
            if (Input.GetKeyUp(KeyCode.R) && !banBuild)
                doStructR = true;
            if (doStructR)
                myStruct = Resources.Load<GameObject>("Prefabs/StructR");
            if (Input.GetKeyUp(KeyCode.Q) && !banBuild)
                doStructQ = true;
            if (doStructQ)
                myStruct = Resources.Load<GameObject>("Prefabs/StructQ");

            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.R))
                banBuild = false;
            Choose();
        }
        if (transform.position == pos && goBuild)
            BuildStruct(myStruct);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform territory = collision.gameObject.transform.Find("MyTerritory");
        if (territory != null && collision.OverlapPoint(pos))
        {
            unit.Moving.isMoving = false;
            goBuild = false;
        }
    }
    */
}
