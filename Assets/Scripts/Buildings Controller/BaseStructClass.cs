using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionCheck))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DoUnits))]
public class BaseStructClass : MonoBehaviour
{
    public SelectionCheck Selection;
    public Health Health;
    public DoUnits Create;
    void Start()
    {
        Selection = gameObject.GetComponent<SelectionCheck>();
        Health = gameObject.GetComponent<Health>();
        Create = gameObject.GetComponent<DoUnits>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
