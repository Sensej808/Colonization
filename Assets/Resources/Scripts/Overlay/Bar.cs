using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public double maxValue;
    public double realValue;
    public GameObject bar;
    public GameObject background;
    public bool constColor = true;
    public void UpdateBar()
    {
        bar.transform.localScale = new Vector3((float)(realValue / maxValue), bar.transform.localScale.y, bar.transform.localScale.z);
        bar.transform.localPosition = new Vector3(background.transform.localPosition.x - background.transform.localScale.x / 2 + bar.transform.localScale.x / 2, bar.transform.localPosition.y, bar.transform.localScale.z);
        if (!constColor)
            UpdateColor();
    }
    public void  UpdateColor()
    {
        if (realValue > maxValue / 4 * 3)
            bar.GetComponent<Renderer>().material.color = Color.green;
        else if (realValue < maxValue / 4 * 3 && realValue > maxValue / 4 * 2)
            bar.GetComponent<Renderer>().material.color = Color.yellow;
        else if (realValue < maxValue / 4 * 2 && realValue > maxValue / 4 * 1)
            bar.GetComponent<Renderer>().material.color = new Color(241f / 255f, 148f / 255f, 15f / 255f);
        else if (realValue < maxValue / 4 * 1)
            bar.GetComponent<Renderer>().material.color = Color.red;
        if (realValue <= 0)
            Destroy(gameObject);
    }    
}
