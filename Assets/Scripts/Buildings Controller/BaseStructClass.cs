using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStructClass : MonoBehaviour
{
    public SelectionCheck Selection;
    public Health Health;
    public DoUnits Create;
    void Start()
    {
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
        Create = gameObject.AddComponent<DoUnits>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
