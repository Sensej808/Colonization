using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс строителей
public class EngineerClass : BaseUnitClass
{
    public Build Builder;
    public new void Start()
    {
        base.Start();
        Builder = gameObject.AddComponent<Build>();
        Attack.attackRange = 1f;
        Attack.cooldown = 1f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/WhiteCircle");
        ProductionTime = 8f;
    }
}
