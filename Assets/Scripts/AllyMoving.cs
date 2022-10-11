using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//������ ������������ ������� ������
public class AllyMoving : MonoBehaviour
{
    private Vector3 finalPos; //�����, ���� ��� ����
    private float speed = 5f; //�������� �����
    private bool isMoving = false; //�������� true, ���� ������� ������� ��� ���� ���� ���, false, ���� ����������� ��� �����
    public SelectionCheck Selection; //����� ��� ��������� � ���������� IsSelected ������ SelectionCheck 
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Selection.isSelected)
            SetPosition();
        if (isMoving)
            Move();
        if (Input.GetKey("s")) //���� �������� �� s, ���� ���������������
            isMoving = false;
    }
    private void SetPosition() //������������� �������� finalPos, ������ IsMoving �� true
    {
        finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPos.z = 0;
        isMoving = true;
    }
    private void Move() //����������� ����� � finalPos
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
        if (transform.position == finalPos)
            isMoving = false;
    }
}
