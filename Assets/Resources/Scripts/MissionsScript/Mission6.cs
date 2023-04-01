using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission6 : MonoBehaviour
{
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Canvas").transform.Find("Timer").Find("Image").Find("Text").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer._timeLeft >= 720)
        {
            MissionController.missionList.Find(x => x.name == "Mission6").isCompleted = true;
            MissionController.CheckMission(MissionController.missionList.Find(x => x.name == "Mission6"));
        }
    }
    void OnDestroy()
    {

        MissionController.missionList.Find(x => x.name == "Mission6").isFailed = true;
        MissionController.CheckMission(MissionController.missionList.Find(x => x.name == "Mission6"));
    }
}
