using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public CommandController controller;
    public CreateSelectionGrid grid;
    public void Update()
    {
        //если мы выделяем юнитов, выделение заканчивается отпусканием ЛКМ, и в этот святой момент
        //мы чекаем, сколько билдеров выделено, если 0, то не открываем buildmenu
        //если не 0, то открываем
        if (grid.SelectionGrid == null)
        {
            int i = 0;
            int k = 0;
            foreach(GameObject go in controller.selectedUnits)
            {
                if (go != null)
                {
                    if (go.GetComponent<Build>())
                        i++;
                    if (go.GetComponent<DoUnits>())
                        k++;
                }
            }
            if (i == 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(false);
            if (i > 0)
                gameObject.transform.Find("BuildMenu").gameObject.SetActive(true);
            if (k == 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(false);
            if (k > 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(true);
        }
    }
}
