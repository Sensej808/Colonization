using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CheckDestroyEnemyBuilding : MonoBehaviour
{
    public float checkRadius;
    public List<GameObject> buildingList;
    public List<GameObject> removeList;
    public Timer timer;
    public string missionName;
    public float loseTime;
    void Start()
    {
        buildingList = Physics2D.OverlapCircleAll(gameObject.transform.position, 20).ToList<Collider2D>().ConvertAll(x => x.gameObject);
        buildingList = buildingList.Distinct().ToList<GameObject>();
        buildingList.RemoveAll(x => (x.tag != "Enemy" && x.GetComponent<Rigidbody2D>() == null));
        buildingList.RemoveAll(x => x.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static);
        timer = GameObject.Find("Canvas").transform.Find("Timer").Find("Image").Find("Text").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in buildingList)
        {
            if (go.IsDestroyed())
                removeList.Add(go);
        }
        if (removeList.Count > 0)
        {
            foreach (GameObject go in removeList)
                buildingList.Remove(go);
            removeList.Clear();
        }
        if (buildingList.Count == 0)
        {
            //print("Mission complete");
            Destroy(gameObject);
            MissionController.missionList.Find(x => x.name == missionName).isCompleted = true;
            MissionController.CheckMission(MissionController.missionList.Find(x => x.name == missionName));
        }
        if (timer._timeLeft >= loseTime)
        {
            MissionController.missionList.Find(x => x.name == missionName).isFailed = true;
            MissionController.CheckMission(MissionController.missionList.Find(x => x.name == missionName));
        }
    }
}
