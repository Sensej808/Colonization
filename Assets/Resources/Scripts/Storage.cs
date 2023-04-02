using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static List<GameObject> selectedUnits;
    public static int amountResources;
    public static MaterialsText text;
    //public static Storage Instance { get; private set; }
    /*
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            selectedUnits = new List<GameObject> ();
            return;
        }
        Destroy(this.gameObject);
    }
    */
    public void Start()
    {
        text = GameObject.Find("Canvas").transform.Find("Recourses").gameObject.GetComponent<MaterialsText>();
        selectedUnits = new List<GameObject>();
        amountResources = 0;
    }
    public static void GetSelectedUnits()
    {
        List<SelectionCheck> ArrSelectComponent = new List<SelectionCheck>(GameObject.FindObjectsOfType<SelectionCheck>());
        ArrSelectComponent = ArrSelectComponent.FindAll(x => x.isSelected);
        //selectedUnits.Clear();
        selectedUnits = ArrSelectComponent.ConvertAll(x => x.gameObject);
        //print(selectedUnits.Count);
        //GameObject.Find("Canvas").GetComponent<OpenMenu>().DrawSelectedUnit(selectedUnits);
    }
    public static void AddResources(int amount)
    {
        amountResources += amount;
        //print(amountResources);
        text.TextUpdate();
    }
    public static int GetResources()
    {
        return amountResources;
    }
    public static void TakeResources(int amount)
    {
        amountResources -= amount;
        text.TextUpdate();
    }
}
