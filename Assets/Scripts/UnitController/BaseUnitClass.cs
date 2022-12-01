using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Базовый класс-интерфейс юнитов
public enum StateUnit { Normal, BuildStruct, GoUseAbility }

[RequireComponent(typeof(AllyMoving))]
[RequireComponent(typeof(SelectionCheck))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BaseAttack))]
public class BaseUnitClass : MonoBehaviour
{
    [HideInInspector]
    public AllyMoving Moving;
    [HideInInspector]
    public SelectionCheck Selection;
    [HideInInspector]
    public Health Health;
    public BaseAttack Attack;
    public StateUnit state;
    public float ProductionTime;
    public virtual void Start()
    {
        Moving = gameObject.GetComponent<AllyMoving>();
        Selection = gameObject.GetComponent<SelectionCheck>();
        Health = gameObject.GetComponent<Health>();
        Attack = gameObject.GetComponent<BaseAttack>();
        state = StateUnit.Normal;

    }
}
