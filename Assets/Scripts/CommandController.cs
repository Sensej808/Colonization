using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public List<GameObject> selectedUnits;
    public bool clickInterface;
    public Dictionary<string, bool> KeyOnMenu;
    public int k;
    public void GetSelectedUnits()
    {
        selectedUnits.Clear();
        GameObject[] arrSelectedUnits = GameObject.FindGameObjectsWithTag("Allied");
        foreach (GameObject go in arrSelectedUnits)
        {
            if (go.GetComponent<SelectionCheck>().isSelected)
                selectedUnits.Add(go);
        }
    }
    public void Start()
    {
        KeyOnMenu = new Dictionary<string, bool>();
        k = 100;
        KeyOnMenu.Add("Q", false);
        KeyOnMenu.Add("R", false);
    }
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            if ((go.transform.position - pos).magnitude < min)
            {
                min = (go.transform.position - pos).magnitude;
                nearest = go;
            }
        }
        return nearest;
    }
    public void OpenInterfaceMenu()
    {
        clickInterface = true;
    }
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
    public void ClickR()
    {
        AllButtonFalse();
        KeyOnMenu["R"] = true;
    }
    public void ClickQ()
    {
        AllButtonFalse();
        KeyOnMenu["Q"] = true;
    }
    public void SetStructR()
    {
            Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            myPos.z = 0;
            GetSelectedUnits();
            List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
            GameObject builder = Nearest(myPos, group);
            if (builder != null)
                builder.GetComponent<Build>().SetStructR(myPos);
    }
    public void SetStructQ()
    {
            Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            myPos.z = 0;
            GetSelectedUnits();
            List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
            GameObject builder = Nearest(myPos, group);
            if (builder != null)
                builder.GetComponent<Build>().SetStructQ(myPos);
    }
    public void StopBuild()
    {
            GetSelectedUnits();
            List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
            foreach (GameObject go in group)
            {
                go.GetComponent<Build>().StopBuild();
            }
    }
    void Update()
    {
        if (!clickInterface)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetStructQ();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SetStructR();
            }
        }
        if (clickInterface)
        {
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["R"])
            {
                SetStructR();
                k = 100;
            }
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["Q"])
            {
                SetStructQ();
                k = 100;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
            StopBuild();
        if (k != 0)
        {
            k--;
            if (k == 0)
                clickInterface = false;
        }
    }
}
