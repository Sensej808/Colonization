using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TankClass : BaseUnitClass
    {
        public new void Start()
        {
            base.Start();
            Attack.attackRange = 7.5f;
            Attack.cooldown = 2f;
            Attack.bulletPattern = Resources.Load<GameObject>("Prefabs/TankBullet");
            ProductionTime = 16f;
            Attack.GetTargetRange = 8f;
        }
    }
