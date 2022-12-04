using System;
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
    public  Action onMovingEnd; //������� ��������� ����
    public Action onMovingStart; //������� ��������� ����
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
            StopMoving();
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
        if (onMovingStart != null) onMovingStart.Invoke();
        isMoving = true;
    }
    public void Move() //����������� ����� � finalPos
    {
        Debug.Log("Moving");
        if (path == null || path.Count == 0)
        {
            StopMoving();
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
        if (transform.position == path[0])
            path.Remove(path[0]);
        
    }

    //��������� ����� � ����� Pos
    public void MoveTo(Vector3 position)
    {
        path = PathFinding.Instance.FindPath(GetComponent<Transform>().position, position);
        isMoving = true;
    }

    //��������� ����� � �������
    public Vector3 MoveTo(GameObject obj)
    {
        Vector3 Mypos = GetComponent<Transform>().position;
        Vector3 ResPos = obj.GetComponent<Transform>().position;
        Vector3 diff = Mypos - ResPos;
        Vector3 offset = (obj.GetComponent<BoxCollider2D>().bounds.extents);
        //TODO: ������� ���������� ������ � ��������
        offset.x = 0;
        if (diff.x < 0)
        {
            offset.x *= -1;
           
        }
        else if (diff.y < 0)
        {
            offset.y *= -1;
        }
        Debug.Log($"Moving to " + obj.name + " Coords: " + (ResPos + offset));
        path = PathFinding.Instance.FindPath(Mypos, ResPos + offset);
        isMoving = true;
        return path[path.Count - 1];       

    }

    //��������� ����
    public void StopMoving()
    {
        //MoveTo(GetComponent<Transform>().position);
        isMoving = false;
        path = null;
        if(onMovingEnd != null)
            onMovingEnd.Invoke();
    }
}
