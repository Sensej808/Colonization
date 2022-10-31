using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//скрипт передвижения союзных юнитов
public class AllyMoving : MonoBehaviour
{
    public Vector3 finalPos; //точка, куда идёт юнит
    private float speed = 5f; //скорость юнита
    public bool isMoving = false; //значение true, если выбрана позиция или юнит туда идёт, false, если остановился или дошёл
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
        if (Input.GetKey("s")) //если нажимаем на s, юнит останавливается
            isMoving = false;
    }
    public void SetPosition()//устанавливает значение finalPos = 0, меняет IsMoving на true
    {
        finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPos.z = 0;
        isMoving = true;
    }
    public void Move() //передвигает юнита к finalPos
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
        if (transform.position == finalPos)
            isMoving = false;
    }
}
