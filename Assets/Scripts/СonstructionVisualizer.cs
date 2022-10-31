using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СonstructionVisualizer : MonoBehaviour
{
    public Vector3 buildPos;
    public bool structBe = false;
    public GameObject flyStruct;
    List<GameObject> engineers;
    public GameObject myStruct;

    public bool selectStructQ;
    public bool doStructQ;

    public bool selectStructR;
    public bool doStructR;
    public void SelectBuildPosition(GameObject flyBuildStruct) //с помощью него выбираем место строительства
    {
        buildPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        buildPos.x = Mathf.RoundToInt(buildPos.x); //округляем до целых, чтобы здания нельзя было строить где попало
        buildPos.y = Mathf.RoundToInt(buildPos.y); //округляем до целых, чтобы здания нельзя было строить где попало
        buildPos.z = 0;
        if (!structBe) //если шаблона здания нет, то создаём его
        {
            flyStruct = Instantiate(flyBuildStruct, buildPos, flyBuildStruct.transform.rotation);
            structBe = true;
        }
        else //если он создан, то передвигаем его
        {
            if (flyStruct != null)
                flyStruct.transform.position = new Vector3(buildPos.x, buildPos.y, buildPos.z);
        }
    }

    public void FinalFunc(ref bool doStruct, ref bool selectStruct)
    {
        if (selectStruct)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Allied");
            engineers = new List<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                if (go.GetComponent<Build>())
                {
                    if (go.GetComponent<SelectionCheck>().isSelected && go.GetComponent<Build>().buildMenuOpen)
                        engineers.Add(go);
                }
            }
            if (engineers.Count > 0)
                SelectBuildPosition(myStruct);
        }
        if (doStruct)
        {
            Destroy(flyStruct);
            structBe = false;
            selectStruct = false;
            doStruct = false;
        }
    }
    public void Choose()
    {
        FinalFunc(ref doStructR, ref selectStructR);
        FinalFunc(ref doStructQ, ref selectStructQ);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            selectStructR = true;
        if (selectStructR)
            myStruct = Resources.Load<GameObject>("Prefabs/FlyStructR");
        if (Input.GetKeyUp(KeyCode.R))
            doStructR = true;

        if (Input.GetKey(KeyCode.Q))
            selectStructQ = true;
        if (selectStructQ)
            myStruct = Resources.Load<GameObject>("Prefabs/FlyStructQ");
        if (Input.GetKeyUp(KeyCode.Q))
            doStructQ = true;
        Choose();
        /*
        if(Input.GetKey("r"))
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Allied");
            engineers = new List<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                if(go.GetComponent<Build>())
                {
                    if(go.GetComponent<SelectionCheck>().isSelected && go.GetComponent<Build>().buildMenuOpen)
                        engineers.Add(go);
                }
            }
            if(engineers.Count > 0)
                SelectBuildPosition(Resources.Load<GameObject>("Prefabs/FlyStructR"));
        }
        if(Input.GetKeyUp("r"))
        {
            Destroy(flyStruct);
            structBe=false;
        }
        */
    }
}
