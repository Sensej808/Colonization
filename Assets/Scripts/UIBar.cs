using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public float realtime;
    public float time;
    public Image image;
    public GameObject canv;
    void Start()
    {
        image.fillAmount = realtime / time;
    }
    private void LateUpdate()
    {
        image.fillAmount = realtime / time;
    }
}
