using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Allied")
        {
            if (GameObject.FindObjectsOfType<Build>().Length > 0)
                MissionController.missionList.Find(x => x.name == "Mission1").isCompleted = true;
            else
                MissionController.missionList.Find(x => x.name == "Mission1").isFailed = true;
            MissionController.CheckMission(MissionController.missionList.Find(x => x.name == "Mission1"));
            Destroy(gameObject);
        }
    }
}
