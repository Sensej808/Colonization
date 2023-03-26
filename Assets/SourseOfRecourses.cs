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
 * ������ ������������ � �����
 * ������ �������� ��������
 * ������ ������� �������
 * 
 * 
 * ������������ � �����:
 * ���� ��������� ������������ � �������, ������� ���������� � �������� ������ �������
 * 
 * ������ �������� �������:
 * ������ �������� ������ ������, ������� ���������� 
 * */