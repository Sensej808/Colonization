using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using static UnityEditor.PlayerSettings;

public class Build : MonoBehaviour
{
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
}
