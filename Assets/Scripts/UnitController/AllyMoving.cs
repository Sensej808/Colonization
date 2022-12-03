using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//������ ������������ ������� ������
public class AllyMoving : MonoBehaviour
{
    public Vector3 finalPos; //�����, ���� ��� ����
    public Vector3 CurPos; // �����, � ������� ������ ���� ����
    public List<Vector3> path; //����, �� �������� ��� ����
    public float speed = 5f; //�������� �����
    public bool isMoving = false; //�������� true, ���� ������� ������� ��� ���� ���� ���, false, ���� ����������� ��� �����
    public BaseUnitClass unit;
    void Start()
    {
        unit = gameObject.GetComponent<BaseUnitClass>();
    }
    void Update()
    {
        if (unit.state != StateUnit.BuildStruct)
        {
            if (Input.GetMouseButtonDown(1) && unit.Selection.isSelected && !EventSystem.current.IsPointerOverGameObject())
            {
                SetPosition();

            }
            if (isMoving)
                Move();
        }
        if (Input.GetKey("s")) //���� �������� �� s, ���� ���������������
            isMoving = false;
    }
    public void SetPosition()//������������� �������� finalPos = 0, ������ IsMoving �� true
    {

        finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PathFinding.Instance.grid.GetXY(finalPos, out int x, out int y);
        finalPos.x = x;
        finalPos.y = y;
        finalPos.z = 0;
        //Debug.Log("!Finpose: " + finalPos.x + " " + finalPos.y);
        path = PathFinding.Instance.FindPath(GetComponent<Transform>().position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        isMoving = true;
    }
    public void Move() //����������� ����� � finalPos
    {
        //Debug.Log("!HERE: " + finalPos.x + " " + finalPos.y);
        //Debug.Log("HERE: " + path[0].x + " " + path[0].y);
        transform.position = Vector3.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
        if (transform.position == path[0])
            path.Remove(path[0]);
        if (path.Count == 0)
            isMoving = false;
    }

    public void MoveTo(Vector3 position)
    {
        path = PathFinding.Instance.FindPath(GetComponent<Transform>().position, position);
        isMoving = true;
    }
}
