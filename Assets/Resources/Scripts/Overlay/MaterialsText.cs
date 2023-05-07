using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialsText : MonoBehaviour
{
    private void Start()
    {
        //RecourceManager.OnRecourcesChanged += TextUpdate;
        //{
        //    TextUpdate();
        //};
        GetComponent<TextMeshProUGUI>().text = "0";
        //TextUpdate();
    }
    public void TextUpdate()
    {
        Debug.Log("TextChanged");

        GetComponent<TextMeshProUGUI>().text = "" + Storage.GetResources();
    }
}
