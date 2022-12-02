using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static List<GameObject> selectedUnits;
    public static Storage Instance { get; private set; }
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
    public static void GetSelectedUnits()
    {
        List<SelectionCheck> ArrSelectComponent = new List<SelectionCheck>(GameObject.FindObjectsOfType<SelectionCheck>());
        ArrSelectComponent = ArrSelectComponent.FindAll(x => x.isSelected);
        //selectedUnits.Clear();
        selectedUnits = ArrSelectComponent.ConvertAll(x => x.gameObject);
        //print(selectedUnits.Count);
    }
    public void Update()
    {
        GetSelectedUnits();
    }
}
