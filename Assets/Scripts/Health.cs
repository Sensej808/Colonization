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
    public GameObject HpBar;

    void Start()
    {
        CurrentHealth = HP;
        HpBar = Instantiate(Resources.Load<GameObject>("Prefabs/Bar"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 1.7f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        HpBar.transform.parent = gameObject.transform;
        HpBar.GetComponent<Bar>().maxValue = HP;
        HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        HpBar.GetComponent<Bar>().bar.GetComponent<Renderer>().material.color = Color.green;
    }

    public void GetDamage(double damage)
    {
        if (CurrentHealth - damage > 0) 
            CurrentHealth -= damage;
        else 
            CurrentHealth = 0;
        HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        HpBar.GetComponent<Bar>().UpdateBar();
    }

    public void GetHealth(double health)
    {
        if (CurrentHealth + health < HP) 
            CurrentHealth += health;
        else 
            CurrentHealth = HP;
        HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        HpBar.GetComponent<Bar>().UpdateBar();
    }
    public void Death()
    {
        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }
    public void Update()
    {
        //HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        Death();
    }
}
