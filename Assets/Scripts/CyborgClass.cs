using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CyborgClass : BaseUnitClass
{
    public CyborgAttack Attack;
    public new void Start()
    {
        base.Start();
        Attack = gameObject.AddComponent<CyborgAttack>();
    }
}
