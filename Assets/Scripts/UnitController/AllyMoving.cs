using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//������ ������������ ������� ������
public class AllyMoving : MonoBehaviour
{
    public Vector3 finalPos; //�����, ���� ��� ����
    private float speed = 5f; //�������� �����
    public bool isMoving = false; //�������� true, ���� ������� ������� ��� ���� ���� ���, false, ���� ����������� ��� �����
    public BaseUnitClass unit;
    void Start()
    {
        unit = gameObject.GetComponent<BaseUnitClass>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && unit.Selection.isSelected)
            SetPosition();
        if (isMoving)
            Move();
        if (Input.GetKey("s")) //���� �������� �� s, ���� ���������������
            isMoving = false;
    }
    public void SetPosition()//������������� �������� finalPos = 0, ������ IsMoving �� true
    {
        finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPos.z = 0;
        isMoving = true;
    }
    public void Move() //����������� ����� � finalPos
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
        if (transform.position == finalPos)
            isMoving = false;
    }
}
