using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DoctorClass : BaseUnitClass
{
    public new void Start()
    {
        base.Start();
        Attack.attackRange = 0f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
        ProductionTime = 8f;
        Attack.GetTargetRange = 0f;
        gameObject.AddComponent<Healing>();
    }
}
