using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СonstructionVisualizer : MonoBehaviour
{
    public Vector3 buildPos;
    public bool structBe = false;
    public GameObject flyStruct;
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
    void Update()
    {
        if(Input.GetKey("b"))
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Allied");
            List<GameObject> engineers = new List<GameObject>();
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
        if(Input.GetKeyUp("b"))
        {
            Destroy(flyStruct);
            structBe=false;
        }
    }
}
