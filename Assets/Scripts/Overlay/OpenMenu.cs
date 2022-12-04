using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public CreateSelectionGrid grid;
    public void Update()
    {
        //���� �� �������� ������, ��������� ������������� ����������� ���, � � ���� ������ ������
        //�� ������, ������� �������� ��������, ���� 0, �� �� ��������� buildmenu
        //���� �� 0, �� ���������
        if (grid.SelectionGrid == null)
        {
            int i = 0;
            int k = 0;
            Storage.GetSelectedUnits();
            foreach(GameObject go in Storage.selectedUnits)
            {
                    if (go.GetComponent<Build>())
                        i++;
                    if (go.GetComponent<DoUnits>())
                        k++;
            }
            if (i == 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(false);
            if (i > 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(true);
            if (k == 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(false);
            if (k > 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.transform.Find("MenuHelp").gameObject.SetActive(!gameObject.transform.Find("MenuHelp").gameObject.activeSelf);

        }
    }
}
