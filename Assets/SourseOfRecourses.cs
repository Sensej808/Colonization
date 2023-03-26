using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourseOfRecourses : Obstacle
{
    public int RecoursesInBeginning = 100;
    public int RecourseRemain;
    public bool IsActive = true;

    private void Start()
    {
        IsActive = true;
        RecourseRemain = RecoursesInBeginning;
        TakePlase();
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