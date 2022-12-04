using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "GameStorage", order = -100)]
public class InGameStorage : ScriptableObject
{
    public InGameStorage Instance;
    [SerializeField]
    public Materials materials;

    [System.Serializable]
    public class Materials
    {
        public int Metal = 0;
        public int Cristalls = 0;
    }
}


