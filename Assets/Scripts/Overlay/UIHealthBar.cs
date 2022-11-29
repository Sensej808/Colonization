using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image img;
    float originalSize;

    //public UIHealthBar MyHealthBar = new UIHealthBar();

    void Start()
    {
        originalSize = img.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
