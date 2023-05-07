using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Общий класс здоровья
public class Health : MonoBehaviour
{
    public double CurrentHealth;
    public double HP = 100;
    public GameObject HpBar;
    public GameObject icon;
    public Action onGetDamage;
    public Action onGetHealth;

    void Start()
    {
        CurrentHealth = HP;
        HpBar = Instantiate(Resources.Load<GameObject>("Prefabs/Bar"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 1.7f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        HpBar.transform.parent = gameObject.transform;
        HpBar.GetComponent<Bar>().maxValue = HP;
        HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        HpBar.GetComponent<Bar>().bar.GetComponent<Renderer>().material.color = Color.green;
        HpBar.GetComponent<Bar>().constColor = false;
        HpBar.transform.localScale = new Vector3(HpBar.transform.localScale.x*0.7f, HpBar.transform.localScale.y*0.6f, HpBar.transform.localScale.z);
    }

    public void GetDamage(double damage)
    {
        if (CurrentHealth - damage > 0) 
            CurrentHealth -= damage;
        else 
            CurrentHealth = 0;
        HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        HpBar.GetComponent<Bar>().UpdateBar();
        if(onGetDamage != null)
            onGetDamage.Invoke();
    }

    public void GetHealth(double health)
    {
        if (CurrentHealth + health < HP) 
            CurrentHealth += health;
        else 
            CurrentHealth = HP;
        HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        HpBar.GetComponent<Bar>().UpdateBar();
        if (onGetHealth != null)
            onGetHealth.Invoke();
    }
    public void Death()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        //HpBar.GetComponent<Bar>().realValue = CurrentHealth;
        Death();
    }
}
