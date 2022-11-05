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
    public double Health = 100;

    void Start()
    {
        CurrentHealth = Health;
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
        if (CurrentHealth + health < Health) 
            CurrentHealth += health;
        else 
            CurrentHealth = Health;
    }
}
