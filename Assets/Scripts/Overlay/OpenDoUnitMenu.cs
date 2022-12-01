using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoUnitMenu : MonoBehaviour
{
    public CommandController controller;
    public void Update()
    {
        //если мы выделяем здания, выделение заканчивается отпусканием ЛКМ, и в этот момент
        //мы чекаем, сколько зданий выделено, если 0, то не открываем меню
        //если не 0, то открываем
        if (Input.GetMouseButtonUp(0))
        {
            int i = 0;
            foreach (GameObject go in controller.selectedUnits)
            {
                if (go.GetComponent<DoUnits>())
                    i++;
            }
            if (i == 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(false);
            if (i > 0)
                gameObject.transform.Find("DoUnitMenu").gameObject.SetActive(true);
        }
    }
}
