using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//скрипт передвижения союзных юнитов
public class AllyMoving : MonoBehaviour
{
    private Vector3 finalPos; //точка, куда идёт юнит
    private float speed = 5f; //скорость юнита
    private bool isMoving = false; //значение true, если выбрана позиция или юнит туда идёт, false, если остановился или дошёл
    public SelectionCheck Selection; //нужен для обращения к переменной IsSelected класса SelectionCheck 
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Selection.isSelected)
            SetPosition();
        if (isMoving)
            Move();
        if (Input.GetKey("s")) //если нажимаем на s, юнит останавливается
            isMoving = false;
    }
    private void SetPosition() //устанавливает значение finalPos, меняет IsMoving на true
    {
        finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPos.z = 0;
        isMoving = true;
    }
    private void Move() //передвигает юнита к finalPos
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
        if (transform.position == finalPos)
            isMoving = false;
    }
}
