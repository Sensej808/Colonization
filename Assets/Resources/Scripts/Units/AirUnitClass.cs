using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirUnitClass : BaseUnitClass
{
    public new void Start()
    {
        base.Start();
        Attack.attackRange = 5f;
        Attack.cooldown = 0.3f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
        ProductionTime = 8f;
        Attack.GetTargetRange = 7.5f;
    }
}