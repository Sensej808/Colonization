using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class СonstructionVisualizer : MonoBehaviour
{
    public Vector3 buildPos;
    public bool structBe = false;
    public GameObject flyStruct;
    List<GameObject> engineers;
    public GameObject myStruct;
    public bool ClickInUIBuildMenu;
    public int k;

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
                    if (go.GetComponent<SelectionCheck>().isSelected)
                        engineers.Add(go);
                }
            }
            if (engineers.Count > 0)
                SelectBuildPosition(myStruct);
        }
        if (doStruct)
        {
            Destroy(flyStruct);
            //structBe = false;
            k = 100;
            //selectStruct = false;
            doStruct = false;
        }
    }
    public void Choose()
    {
        FinalFunc(ref doStructR, ref selectStructR);
        FinalFunc(ref doStructQ, ref selectStructQ);
    }
    public void ClickMouseButton()
    {
        if (ClickInUIBuildMenu && Input.GetMouseButtonDown(0))
        {
            ClickInUIBuildMenu = false;
            if (selectStructR)
                doStructR = true;
            if (selectStructQ)
                doStructQ = true;
        }
    }
    public void ButtonRClickDown()
    {
        selectStructR = true;
        ClickInUIBuildMenu = true;

    }
    public void ButtonQClickDown()
    {
        selectStructQ = true;
        ClickInUIBuildMenu = true;

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
        ClickMouseButton();
        Choose();
        if (k != 0)
        {
            k -= 1;
            if (k == 0)
            {
                structBe = false;
                selectStructQ = false;
                selectStructR = false;
            }
        }
    }
}
