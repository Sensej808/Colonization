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
            return;
        }
        Destroy(this.gameObject);
    }
    public void GetSelectedUnits()
    {
        List<SelectionCheck> ArrSelectComponent = new List<SelectionCheck>(GameObject.FindObjectsOfType<SelectionCheck>());
        ArrSelectComponent = ArrSelectComponent.FindAll(x => x.isSelected);
        //selectedUnits.Clear();
        selectedUnits = ArrSelectComponent.ConvertAll(x => x.gameObject);
    }
    public void Update()
    {
        GetSelectedUnits();
    }
}
