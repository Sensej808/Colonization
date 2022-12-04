using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//скрипт передвижения союзных юнитов
public class AllyMoving : MonoBehaviour
{
    public Vector3 finalPos; //точка, куда идёт юнит
    public Vector3 CurPos; // Точка, к которой сейчас идет юнит
    public List<Vector3> path; //Путь, пл которому идёт юнит
    public float speed = 5f; //скорость юнита
    public bool isMoving = false; //значение true, если выбрана позиция или юнит туда идёт, false, если остановился или дошёл
    public BaseUnitClass unit;
    public  Action onMovingEnd; //Событие окончания пути
    public Action onMovingStart; //Событие окончания пути
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
        if (Input.GetKey("s")) //если нажимаем на s, юнит останавливается
            StopMoving();
    }
    public void SetPosition()//устанавливает значение finalPos = 0, меняет IsMoving на true
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
    public void Move() //передвигает юнита к finalPos
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

    //Отправить юнита в точку Pos
    public void MoveTo(Vector3 position)
    {
        path = PathFinding.Instance.FindPath(GetComponent<Transform>().position, position);
        isMoving = true;
    }

    //Отправить юнита к объекту
    public Vector3 MoveTo(GameObject obj)
    {
        Vector3 Mypos = GetComponent<Transform>().position;
        Vector3 ResPos = obj.GetComponent<Transform>().position;
        Vector3 diff = Mypos - ResPos;
        Vector3 offset = (obj.GetComponent<BoxCollider2D>().bounds.extents);
        //TODO: сделать нормальный подход к объектам
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

    //Закончить идти
    public void StopMoving()
    {
        //MoveTo(GetComponent<Transform>().position);
        isMoving = false;
        path = null;
        if(onMovingEnd != null)
            onMovingEnd.Invoke();
    }
}
