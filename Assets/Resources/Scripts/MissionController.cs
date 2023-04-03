using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    public static List<Mission> missionList;
    public static WinScript Win;
    public static OpenMenu missionMenu;
    public static MissionController Instance { get; private set; }
    public void Start()
    {
        missionList = new List<Mission>();
        Win = GameObject.Find("WInLoseController").GetComponent<WinScript>();
        missionMenu = GameObject.Find("Canvas").GetComponent<OpenMenu>();
        missionMenu.lastpos = 8;
        string scenename = SceneManager.GetActiveScene().name;
        print(SceneManager.GetActiveScene().name);
        switch (scenename)
        {
            case "location1":
                Add(MissionStorage.Mission1);
                break;
            case "location2":
                Add(MissionStorage.Mission4);
                Add(MissionStorage.Mission6);
                break;
            case "location3":

                break;
            case "location4":

                break;
            case "location5":

                break;
            case "location6":

                break;
            case "location7":

                break;
            case "location8":

                break;
            case "location9":

                break;
            case "location10":

                break;
            case "location11":

                break;
            case "location12":

                break;
            case "location13":

                break;
            case "location14":

                break;
            case "location15":

                break;

        }
    }
    public static void Add(Mission m)
    {
        missionList.Add(m);
        missionMenu.AddMission(m);
    }
    public static void CheckMissionList()
    {
        foreach (Mission mission in missionList)
            CheckMission(mission);
    }
    public static void CheckLevel()
    {
        if (missionList.Count == 0)
            Win.CheatingWin();
    }
    public static void CheckMission(Mission m)
    {
        if (m.isFailed)
        {
            Lose();
            return;
        }
        if (m.isCompleted)
        {
            missionList.Remove(m);
            Complete(m);
            CheckLevel();
            return;
        }
    }
    public static void Lose()
    {
        print("You Lose");
        Win.Lose();
    }
    public static void Complete(Mission m)
    {
        missionMenu.CompleteMission(m);
        print("Mission complete");
        if (m.FuncCompleted != null)
        {
            m.FuncCompleted();
        }
    }
}
