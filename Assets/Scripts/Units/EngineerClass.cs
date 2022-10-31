using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ����������
public class EngineerClass : BaseUnitClass
{
    public Build Builder;
    public new void Start()
    {
        base.Start();
        Builder = gameObject.AddComponent<Build>();
        Attack.attackRange = 1f;
        Attack.cooldown = 100f;
        Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/Circle");
    }
}
