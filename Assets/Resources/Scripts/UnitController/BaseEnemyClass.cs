using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseEnemyClass : BaseUnitClass
{
    new void Start()
    {
        base.Start();
        Attack.attackRange = 5f;
        Attack.cooldown = 0.3f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
        ProductionTime = 100f;
        Attack.GetTargetRange = 4.5f;
    }
}
