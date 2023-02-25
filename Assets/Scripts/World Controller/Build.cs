using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ObjectChangeEventStream;
//using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
    public EngineerClass unit;
    public GameObject building; //���������� ������
    public Vector3 pos; //���������� �������������
    public GameObject buildingUnderConstruction;
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
    public void SetFrame()
    {
        Debug.Log("Create a frame");
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        buildingUnderConstruction = Instantiate(building, pos, building.transform.rotation);
    }
    //��� ������� ������
    public void GoBuild()
    {
        unit.Moving.MoveTo(pos);
        unit.Moving.onMovingEnd += GoAndBuild; //������������� �� �������:����� ���� ������, �������� �������������
        unit.state = StateUnit.GoUseAbility;
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
            {
                Debug.Log(transform.position - new Vector3(0.5F, 0.5F, 0));
                if (transform.position - new Vector3(0.5F, 0.5F, 0) == pos)
                {
                    unit.Moving.onMovingEnd -= GoAndBuild;
                    Debug.Log("Builder arrived");
                    SetFrame();
                    building = null;
                    unit.state = StateUnit.BuildStruct;
                    BuildStruct();
                }
                else
                {
                Debug.Log("Go to cons pos");
                GoBuild();
                }
            }

            else
            {
                unit.state = StateUnit.Normal;
                building = null;
                unit.Moving.isMoving = false;
            }
            
        }
    }
    public void BuildStruct()
    {
        if (unit.state == StateUnit.BuildStruct)
        {
            if (buildingUnderConstruction.GetComponent<Frame>().time <= 0)
            {
                Debug.Log("building struct...");
                Instantiate(buildingUnderConstruction.GetComponent<Frame>().futureBuilding, buildingUnderConstruction.transform.position, buildingUnderConstruction.transform.rotation);
                Destroy(buildingUnderConstruction);
                unit.state = StateUnit.Normal;
                StopBuild();
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
        unit.state = StateUnit.Normal;
        if (buildingUnderConstruction != null)
            Destroy(buildingUnderConstruction);
    }
    public void Start()
    {
        unit = gameObject.GetComponent<EngineerClass>();
    }
    public void Update()
    {
        //�� ������ ��� �������, �� �� �������� ������ ���� building �� ������, ����� �� ����� ����������(������ ����)
        //GoAndBuild();
        BuildStruct();
    }
    //��� ����� ��� ����, ����� ���� �� ����� ������� � ��� ���� ��������, � ������ � ������, ���������� �� �����
    //������� � ������, � ����� ������ ������ building � ���� �������
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform territory = collision.gameObject.transform.Find("MyTerritory");
        if (territory != null && collision.OverlapPoint(pos) && buildingUnderConstruction != null)
        {
            if (territory != buildingUnderConstruction.transform.Find("MyTerritory"))
                StopBuild();
        }
    }
}
