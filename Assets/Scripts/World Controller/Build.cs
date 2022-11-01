using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public bool goBuild;
    public Vector3 pos;
    public GameObject myStruct;

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
            if (Input.GetKeyUp(KeyCode.R))
                doStructR = true;
            if (doStructR)
                myStruct = Resources.Load<GameObject>("Prefabs/StructR");
            if (Input.GetKeyUp(KeyCode.Q))
                doStructQ = true;
            if (doStructQ)
                myStruct = Resources.Load<GameObject>("Prefabs/StructQ");
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
}
