using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    public CreateSelectionGrid grid;
    public void Start()
    {
        Time.timeScale = 0;
    }
    public void Update()
    {
        //если мы выделяем юнитов, выделение заканчивается отпусканием ЛКМ, и в этот святой момент
        //мы чекаем, сколько билдеров выделено, если 0, то не открываем buildmenu
        //если не 0, то открываем
        if (grid.SelectionGrid == null)
        {
            int i = 0;
            int k = 0;
            Storage.GetSelectedUnits();
            foreach(GameObject go in Storage.selectedUnits)
            {
                    if (go.GetComponent<Build>())
                        i++;
                    if (go.GetComponent<DoUnits>())
                        k++;
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.transform.Find("HelpMenu").gameObject.SetActive(!gameObject.transform.Find("HelpMenu").gameObject.activeSelf);
            if (Time.timeScale == 0)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
        }
    }
        public void ExitGame()
        {
            Application.Quit();

        }
    public void ExitChangeMenu()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
