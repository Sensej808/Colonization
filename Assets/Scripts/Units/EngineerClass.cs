using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс строителей
public class EngineerClass : BaseUnitClass
{
    public Build Builder;
    public BuildMenu Menu;
    public new void Start()
    {
        base.Start();
        Builder = gameObject.AddComponent<Build>();
        Menu = gameObject.AddComponent<BuildMenu>();
        Attack.attackRange = 1f;
        Attack.cooldown = 100f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
    }
}
