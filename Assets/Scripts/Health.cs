using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: �������� ����� ������ ��������: ������, ������ � �������� ��������� c ��������:
//GetDamage(double damage)
//GetHealth(double health)

//����� ����� ��������
public class Health : MonoBehaviour
{
    public double CurrentHealth;
    public double HP = 100;

    void Start()
    {
        CurrentHealth = HP;
    }

    public void GetDamage(double damage)
    {
        if (CurrentHealth - damage > 0) 
            CurrentHealth -= damage;
        else 
            CurrentHealth = 0;
              
    }

    public void GetHealth(double health)
    {
        if (CurrentHealth + health < HP) 
            CurrentHealth += health;
        else 
            CurrentHealth = HP;
    }
}
