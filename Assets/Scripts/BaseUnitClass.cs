using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseUnitClass : MonoBehaviour
{
    public AllyMoving Moving;
    public SelectionCheck Selection;
    public Health Health;
    public TargetUnitForAttack TakeTargetUnit;
    public virtual void Start()
    {
        Moving = gameObject.AddComponent<AllyMoving>();
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
        TakeTargetUnit = gameObject.AddComponent<TargetUnitForAttack>();
    }
}
