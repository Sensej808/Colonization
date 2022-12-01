using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "GameStorage", order = -100)]
public class InGameStorage : ScriptableObject
{
    [SerializeField]
    public Materials materials;
    //[SerializeField]
    public List<GameObject> PlayerUnits;
    //[SerializeField]
    public List<GameObject> SelectedUnits;
    public class Materials
    {
        int Metal = 0;
        int Log = 0;
    }
}


