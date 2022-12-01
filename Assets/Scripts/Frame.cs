using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public SelectionCheck Selection;
    public Health Health;
    public float constructionTime;
    public GameObject futureBuilding;
    void Start()
    {
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
    }
    void Update()
    {
        constructionTime -= 0.1f;
    }
}
