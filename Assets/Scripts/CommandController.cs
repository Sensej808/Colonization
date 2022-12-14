using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    //public List<GameObject> selectedUnits; //????, ?????????? ?????????? ??????
    public bool clickInterface; //?????????? ??????????, ????? ??? ????? ??????????? ???????: ?? ??????????, ??? ??????????
    public Dictionary<string, bool> KeyOnMenu; //?????? ?????? ??????????, ?????????, ??????? ?????? value true, ???? ?????? ??????
    public int k;

    //???????? ???? ?????????? ??????
    /*
    public void UpdateSelection(List<GameObject> SelectedUnits)
    {
        foreach (var unit in selectedUnits)
        {
            if (unit != null)
            {
                unit.GetComponent<SelectionCheck>().isSelected = false;
                unit.GetComponent<SelectionCheck>().Demonstrate();
            }
        }
        selectedUnits = null;
        selectedUnits = SelectedUnits;
        foreach (var unit in SelectedUnits)
        {
            unit.GetComponent<SelectionCheck>().isSelected = true;
            unit.GetComponent<SelectionCheck>().Demonstrate();
        }
    }
    */
    //???????? ?????????? ????? ?? ??????(?? ?? ???? ??????????), ?? ????????? ? ?????-?? ??????????
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            //if (go != null)
            //{
                if ((go.transform.position - pos).magnitude < min && go.GetComponent<BaseUnitClass>().state == StateUnit.Normal)
                {
                    min = (go.transform.position - pos).magnitude;
                    nearest = go;
                }
            //}
        }
        return nearest;
    }
    //????????? ??? ?????? ??????????
    //(??? ?????, ?????? ??? ? 1 ?????? ????? ?????? ????? 1 ???????)
    public void AllButtonFalse()
    {
        List<string> str = new List<string>();
        foreach (KeyValuePair<string, bool> button in KeyOnMenu)
        {
            str.Add(button.Key);
        }
        foreach (string s in str)
            KeyOnMenu[s] = false;
    }
    //??? ?? ??????? ??? ?????? ???????, ??????? ???????, ????? ?????? ??? ??????
    //(????????? ??? ????? ??????, ?? ?????? ??? ?????????????)

    //???????? ??????  ? ??????????, ???????, ??? ?????? ?????? ??????????
    public void Click(string key)
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu[key] = true;
    }
    //??????? ??????, ????????????? ??????? ? ??????, ?????????? ????????, ????????? ?????????? ?????????? ?????? ??????
    public void SetStruct(string nameStruct)
    {
        Debug.Log("Frame Placed");
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos.z = 0;
        //List<GameObject> group = Storage.selectedUnits.FindAll(x => x != null ? x.GetComponent<Build>() != null : x == null);
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        GameObject builder = Nearest(myPos, group);
        if (builder != null)
        {
            Debug.Log("Going to build");
            builder.GetComponent<Build>().SetStructPos(Resources.Load<GameObject>(nameStruct), myPos);
            builder.GetComponent<Build>().GoAndBuild();
        }
        
    }
    //????????????? ????????????? ???? ?????????? ???????
    public void StopBuild()
    {
        //List<GameObject> group = Storage.selectedUnits.FindAll(x => x != null ? x.GetComponent<Build>() != null : x == null);
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        foreach (GameObject go in group)
            {
                //if (go != null)
                    go.GetComponent<Build>().StopBuild();
            }
    }
    public void AddUnit(string NameUnit)
    {
        //GetSelectedUnits();
        //List<GameObject> group = Storage.selectedUnits.FindAll(x => x != null ? x.GetComponent<DoUnits>() != null : x == null);
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<DoUnits>() != null);
        GameObject myStruct = null;
        if (group.Count != 0)
            myStruct = group.OrderBy(x => x.GetComponent<DoUnits>().queueUnits.Count).First();
        if (myStruct != null)
            myStruct.GetComponent<DoUnits>().AddUnit(Resources.Load<GameObject>(NameUnit));
    }
    public void Start()
    {
        KeyOnMenu = new Dictionary<string, bool>();
        k = 100;
        KeyOnMenu.Add("Q", false); //????????? ?????? Q ? ?????????
        KeyOnMenu.Add("R", false); //????????? ?????? R ? ?????????
        KeyOnMenu.Add("unitW", false);
        KeyOnMenu.Add("unitF", false);
    }

    //?????????? ??????? ???????? ??????? ?? ????????? course
    public void StartMining(GameObject source)
    {
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Mining>() != null);
        foreach (var worker in group)
        {
            worker.GetComponent<Mining>().source = source.GetComponent<SourseOfRecourses>();
            worker.GetComponent<Mining>().enabled = true;
        }
    }

    void Update()
    {
        //???? ?? ?????? ?????? ? ??????????, ?? ????????? ??????? ?? ??????????
        if (!clickInterface)
        {
            //???? ????????? ??????, ????? ??????? ??????
            if (Input.GetKeyUp(KeyCode.Q))
                SetStruct("Prefabs/FrameQ");
            if (Input.GetKeyUp(KeyCode.R))
                SetStruct("Prefabs/FrameR");
            if (Input.GetKeyDown(KeyCode.W))
                AddUnit("Prefabs/Cyborg");
            if (Input.GetKeyDown(KeyCode.F))
                AddUnit("Prefabs/Engineer");

            //????????? ??? ?? ????? ??????????????? ????????? ????

            Ray r = new Ray(Input.mousePosition, Input.mousePosition + new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (Input.GetMouseButtonDown(1) && hit.collider != null)
            {
                var sourse = hit.collider.gameObject;
                //???? ?????? ??? ? ??? ????? ? ???????? ???????? ??? ???????, ?????????? ??????? ????????
                if (sourse.GetComponent<SourseOfRecourses>() != null)
                {
                //Debug.Log(hit.collider.gameObject.name);    
                    StartMining(sourse);

                }
            }
        }
        //???? ?????? ?????? ? ???????????, ?? ????????? ??????? ?? ????? ????
        if (clickInterface)
        {
            //???? ?????? ???, ?? ????? ??????? ???????????????? ??????
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["R"])
            {
                SetStruct("Prefabs/FrameR");
                k = 100;
                AllButtonFalse();
            }
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["Q"])
            {
                SetStruct("Prefabs/FrameQ");
                k = 100;
                AllButtonFalse();
            }
            if (KeyOnMenu["unitW"])
            {
                AddUnit("Prefabs/Cyborg");
                k = 100;
                AllButtonFalse();
            }
            if (KeyOnMenu["unitF"])
            {
                AddUnit("Prefabs/Engineer");
                k = 100;
                AllButtonFalse();
            }
        }
        //???? ???? ESC ??? S(?? ???? stop), ?? ?????????? ??? ??????? ? ?????????? ??????
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            clickInterface = false;
            StopBuild();
        }
        //????????
        //?????? ???, ????? ?? ???? ??? ?? ?????, ?? ??????? ????? ??????
        //? ??? ?? ??????? 5 ???????, ????? ?? ??????? ???? ????????? ?????????, ????? ?????? ??????????(???????, ??? ???? ?????? ????? ?????)
        //?????? ???????? ???????, ? ??? ??? ??? ?????????? ?? ???, ?? ????? ????????? ????? ?????????
        //? ??? ??? ? ??? ?????? ????? ?? ????? ????? 5 ???????, ?? ??? ?????? ?????????????
        //??????? ????????? ????? ????????? ?????, ?? ?????????, ??????? ?? ??????? ???-?? ??? ???
        //?????? ?? ?????? ????? ?? ???????? clickInterface
        //?????? ??? ??????? ???, ??? ?? ???????????, ? ????? ???? ??????? ?????????? ????????? ?????, ??????? ????? ???????
        //??????? ????? ???????? ???? ?? ?????????? ????? ????????? ????? ???????? ?????
        if (k != 0)
        {
            k--;
            if (k == 0)
            {
                clickInterface = false;
            }
        }
    }
}
