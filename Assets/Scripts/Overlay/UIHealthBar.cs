using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Health health;
    public Image image;
    public GameObject canv;
    void Start()
    {
        health = canv.transform.parent.gameObject.GetComponent<Health>();
        image.fillAmount = (float)health.CurrentHealth / (float)health.HP;
    }
    private void LateUpdate()
    {
        if (image != null && health != null)
            image.fillAmount = (float)health.CurrentHealth / (float)health.HP;
    }
}
