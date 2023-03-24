using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public double maxValue;
    public double realValue;
    public GameObject bar;
    public GameObject background;
    public void UpdateBar()
    {
        bar.transform.localScale = new Vector3((float)(realValue / maxValue), bar.transform.localScale.y, bar.transform.localScale.z);
        bar.transform.localPosition = new Vector3(background.transform.localPosition.x - background.transform.localScale.x / 2 + bar.transform.localScale.x / 2, bar.transform.localPosition.y, bar.transform.localScale.z);
    }
}
