using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3 : MonoBehaviour
{
    public void OnDestroy()
    {
        MissionController.missionList.Find(x => x.name == "Mission3").isCompleted = true;
        MissionController.CheckMission(MissionController.missionList.Find(x => x.name == "Mission3"));
    }
}
