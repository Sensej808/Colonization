using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Abba", order = -100)]
public class ExampleConfig : ScriptableObject
{
    public List<UnitInfo> datas = new List<UnitInfo>();

    [System.Serializable]
    public class DataEx
    {
        public int value = 0;
        public string str = string.Empty;
    }

    [System.Serializable]
    public class UnitInfo
    {
        public string tag = string.Empty;
        public DataEx data;
    }
}


