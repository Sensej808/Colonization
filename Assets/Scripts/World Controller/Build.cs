using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public bool goBuild;
    public Vector3 pos;
    public GameObject myStruct;
    public bool buildMenuOpen;
    public void BuildStruct(GameObject building)
    {
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        GameObject territory = building.transform.Find("MyTerritory").gameObject;
        Collider2D[] strangersObjects = Physics2D.OverlapBoxAll(pos, territory.transform.localScale, 0);
        List<Collider2D> listTerritory = new List<Collider2D>();
        foreach(Collider2D go in strangersObjects)
        {
            if (go.gameObject.name == "MyTerritory")
                listTerritory.Add(go);
        }
        if (listTerritory.Count == 0)
            Instantiate(building, pos, building.transform.rotation);
    }
    public void GotoBuildPos()
    {
        unit.Moving.finalPos = pos;
        unit.Moving.isMoving = true;
    }
    public bool isFreePosition(GameObject building)
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
    public void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
        goBuild = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown("b") && unit.Selection.isSelected)
            buildMenuOpen = true;
        if (!unit.Selection.isSelected)
            buildMenuOpen = false;
        if (buildMenuOpen)
        {
            if (unit.Selection.isSelected && Input.GetKeyUp("b"))
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                bool free = isFreePosition(myStruct);
                if (free)
                {
                    GotoBuildPos();
                    goBuild = true;
                }
            }
            if (transform.position == pos && goBuild)
                BuildStruct(myStruct);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform territory = collision.gameObject.transform.Find("MyTerritory");
        if (territory != null && collision.OverlapPoint(pos))
        {
            unit.Moving.isMoving = false;
            goBuild = false;
        }
    }
    /*
    public Vector3 buildPos; //����� �������������
    public bool structBe = false; //������ �� ������ ������
    public GameObject flyStruct; //������ ������
    public void SelectBuildPosition(GameObject flyBuildStruct) //� ������� ���� �������� ����� �������������
    {
        buildPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        buildPos.x = Mathf.RoundToInt(buildPos.x); //��������� �� �����, ����� ������ ������ ���� ������� ��� ������
        buildPos.y = Mathf.RoundToInt(buildPos.y); //��������� �� �����, ����� ������ ������ ���� ������� ��� ������
        buildPos.z = 0;
        if (!structBe) //���� ������� ������ ���, �� ������ ���
        {
            flyStruct = Instantiate(flyBuildStruct, buildPos, flyBuildStruct.transform.rotation);
            structBe = true;
        }
        else //���� �� ������, �� ����������� ���
        {
            if (flyStruct != null)
                flyStruct.transform.position = new Vector3(buildPos.x, buildPos.y, buildPos.z);
        }
    }
    public void BuildStruct(GameObject BuildStruct) //� ������� ��� ������ ������
    {
        if (flyStruct.tag == "IsFreePosition" && flyStruct != null) //���� ����� ��������, �� ������ ������
            Instantiate(BuildStruct, buildPos, flyStruct.transform.rotation);
            Destroy(flyStruct); //������� ������
            structBe = false;
    }
    */
}
