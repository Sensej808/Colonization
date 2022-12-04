using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourseOfRecourses : MonoBehaviour
{
    public int RecoursesInBeginning = 100;
    public int RecourseRemain;
    public bool IsActive = true;
    public int SizeX = 1;
    public int SizeY = 1;


    private void Start()
    {
        IsActive = true;
        RecourseRemain = RecoursesInBeginning;
        PathFinding.Instance.grid.GetXY(transform.position - new Vector3(SizeX / 2, SizeY / 2, 0), out int x, out int y);
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {

                //Debug.Log($"pf = null: {PathFinding.Instance == null} \n grid = null: {PathFinding.Instance.grid == null} \n GetValue = null: {PathFinding.Instance.grid.GetValue(new Vector3(x + i, y + j, 0)) == null}");
                PathFinding.Instance.grid.GetValue(x + i, y + j).SetWalkable(false);
            }
        }
    }
    public int TakeRecourses(int Portion)
    {
        if(RecourseRemain >= Portion)
            RecourseRemain -= Portion;
        else
        {
            Portion = RecourseRemain;
            IsActive = false;
        }

        return Portion;
    }


}
/*
 * Майнер отправляется к шахте
 * Майнер начинает добывать
 * Майнер относит ресурсы
 * 
 * 
 * Отправляется к шахте:
 * надо прописать передвижение к объекту, которое направляет к соседней клетке объекта
 * 
 * Майнер добывает ресурсы:
 * Майнер вызывает таймер добычи, который возвращает 
 * */