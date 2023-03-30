using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class DoctorClass : BaseUnitClass
{
    public Healing healing;
    public new void Start()
    {
        base.Start();
        Attack.attackRange = 0f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
        ProductionTime = 8f;
        Attack.GetTargetRange = 0f;
        healing = gameObject.AddComponent<Healing>();
        healing.prefabHealingBeam = Resources.Load<GameObject>("Prefabs/HealingBeam");
        healing.treatment = 2;
        healing.cooldown = 0.06f;
    }
}
