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
    AudioSource audioSource;
    void Start()
    {
        
        unit = gameObject.GetComponent<BaseUnitClass>();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.GetComponent<BaseAttack>() != null)
            onMovingEnd += gameObject.GetComponent<BaseAttack>().DoIsMoving;
    }
    void Update()
    {
        if (unit.state != StateUnit.BuildStruct)
        {
            if (Input.GetMouseButtonDown(1) && unit.Selection.isSelected && !EventSystem.current.IsPointerOverGameObject())
            {
                SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            }
            if (isMoving)
                Move();
        }
        if (Input.GetKey("s")) //���� �������� �� s, ���� ���������������
            StopMoving();
    }
    public void SetPosition(Vector3 pos)//������������� �������� finalPos = 0, ������ IsMoving �� true
    {

        finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPos.z = 0;
        List<Collider2D> gos = new List<Collider2D>(Physics2D.OverlapCircleAll(finalPos, 0.01f));
            if (null == gos.FindAll(x => x.gameObject.GetComponent<Rigidbody2D>() != null).Find(x => x.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static))
            {
                PathFinding.Instance.grid.GetXY(finalPos, out int x, out int y);
                finalPos.x = x;
                finalPos.y = y;
                finalPos.z = 0;
                //Debug.Log("!Finpose: " + finalPos.x + " " + finalPos.y);
                if (gameObject.layer != 7)
                {
                    path = PathFinding.Instance.FindPath(GetComponent<Transform>().position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                else
                {
                    path = new List<Vector3>();
                    var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    v.z = 0;
                    path.Add(v);
                }    
                if (onMovingStart != null) onMovingStart.Invoke();
                isMoving = true;
            }
    }
    public void Move() //����������� ����� � finalPos
    {
        //Debug.Log("Moving");
        if (path == null || path.Count == 0)
        {
            StopMoving();
            return;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
        Debug.Log(CheckNextCell());
        if (transform.position == path[0])
            path.Remove(path[0]);
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    //��������� ��������� ����� � �������� �� �����������
    public bool CheckNextCell()
    {
        var obs = Physics2D.OverlapBoxAll(new Vector2(path[0].x, path[0].y), new Vector2(PathFinding.Instance.grid.CellSize, PathFinding.Instance.grid.CellSize), 0);

        bool CanMove = true;
        foreach(var col in obs)
        {
            if (!col.isTrigger && col != unit.GetComponent<BoxCollider2D>())
            {
                CanMove = false;
                break;
            }
        }

        Debug.Log($"len = { obs.Length}");
        if (obs.Length == 0)
            return CanMove;
        else
            return CanMove;
    }

    //��������� ����� � ����� Pos
    public void MoveTo(Vector3 position)
    {
        List<Collider2D> gos = new List<Collider2D>(Physics2D.OverlapCircleAll(finalPos, 0.01f));
        if (null == gos.FindAll(x => x.gameObject.GetComponent<Rigidbody2D>() != null).Find(x => x.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static))
        {
            if (gameObject.layer != 7) //���� �� �������� ����
            {
                path = PathFinding.Instance.FindPath(GetComponent<Transform>().position, position);
                isMoving = true;
            }
            else
            {
                path = new List<Vector3>();
                path.Add(position);
                isMoving = true;
            }
        }
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
            if (path != null && path.Count > 0)
                return path[path.Count - 1];
            else
                return transform.position;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(path[0], Vector3.one * PathFinding.Instance.grid.CellSize);
    }
}
