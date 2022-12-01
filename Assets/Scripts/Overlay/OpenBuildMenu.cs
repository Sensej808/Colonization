using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenBuildMenu : MonoBehaviour
{
    public CommandController controller;
    public void Update()
    {
        //���� �� �������� ������, ��������� ������������� ����������� ���, � � ���� ������ ������
        //�� ������, ������� �������� ��������, ���� 0, �� �� ��������� buildmenu
        //���� �� 0, �� ���������
        if (Input.GetMouseButtonUp(0))
        {
            int i = 0;
            foreach(GameObject go in controller.selectedUnits)
            {
                if (go.GetComponent<Build>())
                    i++;
            }
            if (i == 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(false);
            if (i > 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(true);
        }
    }
}
