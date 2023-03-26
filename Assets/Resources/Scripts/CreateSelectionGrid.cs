using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//������� ����� ��������� ������
public class CreateSelectionGrid : MonoBehaviour
{
    public GameObject prefabSelectionGrid; //������ ����� ���������
    public GameObject prefabAirSelectionGrid;
    private Vector3 pos1; //������� ���� �� ������, ��� ������ ������� ���
    private Vector3 pos2; //������� ���� �� ������, �� ����� ������������� ����� ���������
    private Vector3 rpos1;//������� ���� �� �����, ��� ������ ������� ���
    private Vector3 rpos2;//������� ���� �� �����, �� ����� ������������� ����� ���������
    private Vector3 posc; //����� ����� ���������
    private bool selecting = false; //false - ���� ����� ������ ���, true - ���� ����� �������
    public GameObject SelectionGrid; //����� ��������� �� ������
    public GameObject AirSelectionGrid; //����� ��������� �� ������
    public CommandController command;
    void Update()
    {
        if (!Input.GetKey(KeyCode.A) &&Input.GetMouseButtonDown(0) && !command.clickInterface && !EventSystem.current.IsPointerOverGameObject())//��� ������ �������� ���������
        {
            pos1 = Input.mousePosition;
            //Debug.Log("START");
            selecting = true;
            SelectionGrid = Instantiate(prefabSelectionGrid, pos1,transform.rotation); //�������� �����
            AirSelectionGrid = Instantiate(prefabAirSelectionGrid, pos1, transform.rotation); //�������� �����
            rpos1 = Camera.main.ScreenToWorldPoint(pos1); //������� ���� �� �����, ��� ������ ������� ���

        }
        else if (Input.GetMouseButton(0) && selecting) // ����� �������� �����,���� ������ �����
        {
            //Debug.Log("Continue");
            pos2 = Input.mousePosition;
            rpos2 = Camera.main.ScreenToWorldPoint(pos2); //������� ���� �� �����, �� ����� ������������� ����� ���������
            CenterPos(); //������� �����
            SelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);//������ ��������������, ��-�� ���������������� ����������
            SelectionGrid.transform.localScale = rpos2 - rpos1;//������ ������ �����
            AirSelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);//������ ��������������, ��-�� ���������������� ����������
            AirSelectionGrid.transform.localScale = rpos2 - rpos1;//������ ������ �����
            Debug.Log(rpos2 - rpos1);
        }
        else if(Input.GetMouseButtonUp(0) && selecting)
        {
            SelectionGrid.GetComponent<Select>().IsDone = true;//������������ �������� � ��������� ���������
            AirSelectionGrid.GetComponent<Select>().IsDone = true;//������������ �������� � ��������� ���������
            //Debug.Log("FIN");
            selecting = false;
            foreach (GameObject x in SelectionGrid.GetComponent<Select>().SelectedUnits)
            {
                AirSelectionGrid.GetComponent<Select>().SelectedUnits.Add(x);
            }
            Destroy(SelectionGrid);
            Destroy(AirSelectionGrid);
        }

    }
    void CenterPos()//��������, ������ ����� ����� ���������
    {
        posc.x = pos2.x - (pos2.x - pos1.x) / 2;
        posc.y = pos1.y - (pos1.y - pos2.y) / 2;
        posc.z = pos1.z;
        posc = Camera.main.ScreenToWorldPoint(posc);
        posc.z = pos1.z;
    }
}

/*
 * ���� ����� ���, �� 
 *      ������� ������ �����,
 *      ������� ����� ����� � 
 *      ������� � ������, 
 *      � ������ � �����, 
 *      ��� ��������� ������
 *      �������� ���� ������ � �����
 * ����� ������
 */

/*if (!gridIsBe)
            {
                pos1 = Input.mousePosition;
                pos2 = pos1;
            }
            else
                pos2 = Input.mousePosition;
            CenterPos();
            rpos1 = Camera.main.ScreenToWorldPoint(pos1); //������� ���� �� �����, ��� ������ ������� ���
            rpos2 = Camera.main.ScreenToWorldPoint(pos2); //������� ���� �� �����, �� ����� ������������� ����� ���������
            length = (rpos2.x - rpos1.x);
            width = (rpos1.y - rpos2.y);
            if (!gridIsBe)
            {
                realSelectionGrid = Instantiate(prefabSelectionGrid, posc, transform.rotation);
                var s = realSelectionGrid.GetComponent<Select>(); //������ ������ �� ����� ����� ���, �� ��� ����� ������-�� �� ��������
                s.k = true;
            }
            realSelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);
            realSelectionGrid.transform.localScale = new Vector3(length, width, pos1.z);
            gridIsBe = true;

 
 if (SelectionGrid != null)
            {
                var s = SelectionGrid.GetComponent<Select>(); //���� �����, ��� ����, �� �����
                s.k = false;
            }*/