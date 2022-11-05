using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Написать общий скрипт здоровья: Юнитов, зданий и объектов окружения c методами:
//GetDamage(double damage)
//GetHealth(double health)

//Общий класс здоровья
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
