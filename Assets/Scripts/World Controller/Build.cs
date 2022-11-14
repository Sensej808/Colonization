using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public GameObject building; //���������� ������
    public Vector3 pos; //���������� �������������
    //����������, �������� �� ����� �������������
    public bool isFreePosition()
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        GameObject territory = building.transform.Find("MyTerritory").gameObject;
        Collider2D[] strangersObjects = Physics2D.OverlapBoxAll(pos, territory.transform.localScale, 0);
        List<Collider2D> listTerritory = new List<Collider2D>();
        foreach (Collider2D go in strangersObjects)
        {
            if (go.gameObject.name == "MyTerritory")
                listTerritory.Add(go);
        }
        return (listTerritory.Count == 0);
    }
    //������ ������
    public void BuildStruct()
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        Instantiate(building, pos, building.transform.rotation);
    }
    //��� ������� ������
    public void GoBuild()
    {
        unit.Moving.finalPos = pos;
        unit.Moving.isMoving = true;
    }
    //���������� ���������� �������
    //���� ������ �������, �� ���� �� ������
    // �� �������, �������� �� ��������
    //���� ��, ��� �������, ����� ����������� ��, ������� ���������� ������
    //���� �����, � ��� �� ��� ��� �������� ����������, �� ������, ������ ��������, �����������
    public void GoAndBuild()
    {
        if (building != null)
        {
            if (isFreePosition())
                GoBuild();
            else
                building = null;
            if (transform.position == pos && isFreePosition())
            {
                BuildStruct();
                building = null;
            }
        }
    }
    //������������� �������� ������, ������� ����� �������, � ����������, ��� ����� �������
    public void SetStructPos(GameObject myBuilding, Vector3 myPos)
    {
        building = myBuilding;
        pos = myPos;
    }
    //���������� �������������
    public void StopBuild()
    {
        unit.Moving.isMoving = false;
        building = null;
    }
    public void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
    }
    public void Update()
    {
        //�� ������ ��� �������, �� �� �������� ������ ���� building �� ������, ����� �� ����� ����������
        GoAndBuild();
    }
    //��� ����� ��� ����, ����� ���� �� ����� ������� � ��� ���� ��������, � ������ � ������, ���������� �� �����
    //������� � ������, � ����� ������ ������ building � ���� �������
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform territory = collision.gameObject.transform.Find("MyTerritory");
        if (territory != null && collision.OverlapPoint(pos))
        {
            StopBuild();
        }
    }
}
