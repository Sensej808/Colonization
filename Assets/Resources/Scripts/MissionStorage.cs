using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStorage : MonoBehaviour
{
    public static MissionStorage Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    public static void AddMission2()
    {
        MissionController.Add(Mission2);
        foreach (Build b in GameObject.FindObjectsOfType<Build>())
        {
            b.onBuildingEnd += CompleteMission2;
        }
    }
    public static void CompleteMission2()
    {
        MissionController.missionList.Find(x => x.name == "Mission2").isCompleted = true;
        foreach (Build b in GameObject.FindObjectsOfType<Build>())
        {
            b.onBuildingEnd -= CompleteMission2;
        }
        MissionController.CheckMission(MissionController.missionList.Find(x => x.name == "Mission2"));
    }
    public static void AddMission3()
    {
        MissionController.Add(Mission3);
    }
    public static Mission Mission1 = new Mission(false, false, "Дойтите до места строительства базы", "Хотя бы 1 рабочий должен выжить", AddMission2, null, "Mission1");
    public static Mission Mission2 = new Mission(false, false, "Постройте здание", "", AddMission3, null, "Mission2");
    public static Mission Mission3 = new Mission(false, false, "Уничтожте главное здание врага", "", null, null, "Mission3");
    
}
