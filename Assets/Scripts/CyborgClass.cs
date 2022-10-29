using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class CyborgClass : BaseUnitClass
{
    public new void Start()
    {
        base.Start();
        Attack.attackRadius = 5f;
        Attack.cooldown = 100f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/WhiteCircle");
    }
}
