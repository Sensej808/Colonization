using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenBuildMenu : MonoBehaviour
{
    public SelectedUnits selecter;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            selecter.GetOpenMenuEngineers();
            if (selecter.OpenMenuEngineers.Count > 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(true);
            else if (selecter.OpenMenuEngineers.Count == 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(false);
        }
    }
}
