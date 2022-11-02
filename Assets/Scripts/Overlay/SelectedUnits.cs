using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public List<GameObject> OpenMenuEngineers;
    public ÑonstructionVisualizer visualizer;
    public void GetOpenMenuEngineers()
    {
        OpenMenuEngineers.Clear();
        GameObject[] arrSelectedUnits = GameObject.FindGameObjectsWithTag("Allied");
        foreach (GameObject go in arrSelectedUnits)
        {
            if (go.GetComponent<Build>())
            {
                if (go.GetComponent<SelectionCheck>().isSelected)
                    OpenMenuEngineers.Add(go);
            }
        }
    }
    public void DoStructTrue()
    {
        foreach (GameObject en in OpenMenuEngineers)
        {
            if (en != null)
            {
                Build builder = en.GetComponent<Build>();
                if (visualizer.selectStructR)
                    builder.doStructR = true;
                if (visualizer.selectStructQ)
                    builder.doStructQ = true;
            }
        }
    }
    void Update()
    {
        if (visualizer.structBe && Input.GetMouseButtonDown(0))
            DoStructTrue();
    }
}
