using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

//Класс обычного солдата
public class CyborgClass : BaseUnitClass
{
    public new void Start()
    {
        base.Start();
        Attack.attackRange = 5f;
        Attack.cooldown = 0.3f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
        ProductionTime = 8f;
    }
}
