using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityVisualizer : MonoBehaviour
{
    public CommandController controller;
    public Vector3 abilityPos;
    public GameObject flyAbility; //летающая способность
    public GameObject myFlyAbility; //шаблон летающей способности
    public List<GameObject> group;
    //создание летающей способности
    public void CreateFlyAbility()
    {
        abilityPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        abilityPos.z = 0;
        flyAbility = Instantiate(myFlyAbility, abilityPos, myFlyAbility.transform.rotation);
    }
    //передвижение летающей способности
    public void MovingFlyAbility()
    {
        abilityPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        abilityPos.z = 0;
        flyAbility.transform.position = new Vector3(abilityPos.x, abilityPos.y, abilityPos.z);
    }
    //уничтожение летающей способности
    public void DestroyFlyAbility()
    {
        Destroy(flyAbility);
        myFlyAbility = null;
    }
    //логика вызова передвижения и строения
    public void CreateMoving()
    {
        if (myFlyAbility != null)
        {
            if (flyAbility == null)
                CreateFlyAbility();
            else
                MovingFlyAbility();
        }
    }
    public delegate void AddInGroup();
    //устанавливает способность, если конечно для этого есть выделенные юниты
    public void SetMyFlyAbility(GameObject ability, AddInGroup f)
    {
        f();
        if (group.Count > 0)
            myFlyAbility = ability;
    }
    //заносит в group выделенных строителей
    public void AddEngineerInGroup()
    {
        //group = controller.selectedUnits.FindAll(x => x != null ? x.GetComponent<Build>() != null : x == null);
        group = Storage.selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
    }
    public void Start()
    {
        group = new List<GameObject>();
    }
    public void Update()
    {
        //такая же логика как в CommandController
        if (!controller.clickInterface)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SetMyFlyAbility(Resources.Load<GameObject>("Prefabs/FlyStructR"), AddEngineerInGroup);
            if (Input.GetKeyDown(KeyCode.Q))
                SetMyFlyAbility(Resources.Load<GameObject>("Prefabs/FlyStructQ"), AddEngineerInGroup);

            if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.Q))
                DestroyFlyAbility();
        }
        if (controller.clickInterface)
        {
            if (controller.KeyOnMenu["R"])
                SetMyFlyAbility(Resources.Load<GameObject>("Prefabs/FlyStructR"), AddEngineerInGroup);
            if (controller.KeyOnMenu["Q"])
                SetMyFlyAbility(Resources.Load<GameObject>("Prefabs/FlyStructQ"), AddEngineerInGroup);

            if (Input.GetMouseButtonDown(0) && myFlyAbility != null)
                DestroyFlyAbility();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
            DestroyFlyAbility();
        CreateMoving();
    }
}
