using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoUnitMenu : MonoBehaviour
{
    public CommandController controller;
    public void Update()
    {
        //���� �� �������� ������, ��������� ������������� ����������� ���, � � ���� ������
        //�� ������, ������� ������ ��������, ���� 0, �� �� ��������� ����
        //���� �� 0, �� ���������
        if (Input.GetMouseButtonUp(0))
        {
            int i = 0;
            foreach (GameObject go in controller.selectedUnits)
            {
                if (go.GetComponent<DoUnits>())
                    i++;
            }
            if (i == 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(false);
            if (i > 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(true);
        }
    }
}
