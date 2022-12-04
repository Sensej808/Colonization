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
    public double HP = 100;

    UIHealthBar myHealthBar;

    void Start()
    {
        CurrentHealth = HP;
        myHealthBar = GetComponent<UIHealthBar>();
    }

    public void GetDamage(double damage)
    {
        if (CurrentHealth - damage > 0) 
            CurrentHealth -= damage;
        else 
            CurrentHealth = 0;
        //myHealthBar.SetValue((float)(CurrentHealth / HP));
    }

    public void GetHealth(double health)
    {
        if (CurrentHealth + health < HP) 
            CurrentHealth += health;
        else 
            CurrentHealth = HP;

        //myHealthBar.SetValue((float)(CurrentHealth / HP));
    }
    public void Death()
    {
        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }
    public void Update()
    {
        Death();
    }
}
