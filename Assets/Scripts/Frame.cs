using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public SelectionCheck Selection;
    public Health Health;
    public float time;
    public GameObject futureBuilding;
    public GameObject TimeBar;
    private IEnumerator StartTimer()
    {
        while (true)
        {
            time -= Time.deltaTime;
            TimeBar.GetComponent<Bar>().realValue = time;
            TimeBar.GetComponent<Bar>().UpdateBar();
            yield return null;
        }
    }
    void Start()
    {
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
        TimeBar = Instantiate(Resources.Load<GameObject>("Prefabs/Bar"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2.2f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        StartCoroutine(StartTimer());
        TimeBar.transform.parent = gameObject.transform;
        TimeBar.GetComponent<Bar>().maxValue = time;
        TimeBar.GetComponent<Bar>().realValue = time;
        TimeBar.GetComponent<Bar>().bar.GetComponent<Renderer>().material.color = Color.blue;
        TimeBar.GetComponent<Bar>().UpdateBar();
    }
    private void Update()
    {
    }
}
