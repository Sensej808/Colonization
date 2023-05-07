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
        GameObject.Find("EnemyAttackController").GetComponent<location1EnemyAttack>().StartStartAttack();
    }
    public static void AddMission3()
    {
        MissionController.Add(Mission3);
    }
    public static void AddMission5()
    {
        MissionController.Add(Mission5);
        GameObject.Find("CheckMissionController").GetComponent<ActiveHelpMission5>().help.SetActive(true);
        GameObject.Find("HelpMission1").SetActive(false);
        GameObject.Find("CheckMissionController").GetComponent<ActiveHelpMission5>().GoAttack();
    }
    public static void StartAttack()
    {
        GameObject.Find("EnemyAttackController").GetComponent<location1EnemyAttack>().StartStartAttack();
    }
    public static Mission Mission1 = new Mission(false, false, "������� �� ����� ������������� ����(������ ������)", "���� �� 1 ������� ������ ������", AddMission2, null, "Mission1");
    public static Mission Mission2 = new Mission(false, false, "��������� ������", "", AddMission3, null, "Mission2");
    public static Mission Mission3 = new Mission(false, false, "���������� ������� ������ �����(������� ���������)", "", null, null, "Mission3");
    public static Mission Mission4 = new Mission(false, false, "���������� ��� ������ �� ���� ����������", "��� ���� ������� �� 4 ������", AddMission5, null, "Mission4");
    public static Mission Mission5 = new Mission(false, false, "���������� ��� ������ �� ���� ����������", "��� ���� ������� �� 8 ������", StartAttack, null, "Mission5");
    public static Mission Mission6 = new Mission(false, false, "�������� ��� ������� ������ �� ���� �� 12 ������(�������)", "", null, null, "Mission6");

}
