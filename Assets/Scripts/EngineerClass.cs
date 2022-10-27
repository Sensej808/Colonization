using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerClass : BaseUnitClass
{
    public Build Builder;
    public BuildMenu Menu;
    public new void Start()
    {
        base.Start();
        Builder = gameObject.AddComponent<Build>();
        Menu = gameObject.AddComponent<BuildMenu>();
        TakeTargetUnit.attackRadius = 1f;
        TakeTargetUnit.cooldown = 100f;
        TakeTargetUnit.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
    }
}
