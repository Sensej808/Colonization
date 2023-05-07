using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public Health health;
    void Start()
    {
        ChangeColor();
    }
    public void ChangeColor()
    {
            if (health.CurrentHealth > health.HP / 4 * 3)
                gameObject.GetComponent<Image>().color = Color.green;
            else if (health.CurrentHealth < health.HP / 4 * 3 && health.CurrentHealth > health.HP / 4 * 2)
                gameObject.GetComponent<Image>().color = Color.yellow;
            else if (health.CurrentHealth < health.HP / 4 * 2 && health.CurrentHealth > health.HP / 4 * 1)
                gameObject.GetComponent<Image>().color = new Color(241f / 255f, 148f / 255f, 15f / 255f);
            else if (health.CurrentHealth < health.HP / 4 * 1)
                gameObject.GetComponent<Image>().color = Color.red;
            if (health.CurrentHealth <= 0)
                Destroy(gameObject);
    }
    public void OnDestroy()
    {
        health.onGetDamage -= ChangeColor;
        health.onGetHealth -= ChangeColor;
    }
}
