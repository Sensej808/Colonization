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
    public GameObject hpBar;
    public GameObject timeBar;
    private IEnumerator StartTimer()
    {
        while (true)
        {
            time -= Time.deltaTime;
            yield return null;
        }
    }
    void Start()
    {
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
        timeBar = Instantiate(Resources.Load<GameObject>("Prefabs/BarCanvas1"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 1.7f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        timeBar.name = "TimeBar";
        timeBar.transform.parent = gameObject.transform;
        timeBar.transform.Find("background").GetComponent<UIBar>().time = time;
        timeBar.transform.Find("background").GetComponent<UIBar>().realtime = time;
        hpBar = Instantiate(Resources.Load<GameObject>("Prefabs/BarCanvas"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 1.4f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        hpBar.name = "HpBar";
        hpBar.transform.parent = gameObject.transform;
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        timeBar.transform.Find("background").GetComponent<UIBar>().realtime = time;
    }
}
