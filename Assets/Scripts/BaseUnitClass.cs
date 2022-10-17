using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitClass : MonoBehaviour
{
    public AllyMoving Moving;
    public SelectionCheck Selection;
    public HealthScript Health;
    public virtual void Start()
    {
        Moving = gameObject.AddComponent<AllyMoving>();
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<HealthScript>();
    }
}
