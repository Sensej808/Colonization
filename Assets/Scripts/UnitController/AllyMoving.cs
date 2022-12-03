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
        isMoving = true;
    }
    public void Move() //передвигает юнита к finalPos
    {
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
