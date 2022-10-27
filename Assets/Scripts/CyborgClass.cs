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
        TakeTargetUnit.attackRadius = 5f;
        TakeTargetUnit.cooldown = 100f;
        TakeTargetUnit.bulletPattern = Resources.Load<GameObject>("Prefabs/WhiteCircle");
    }
}
