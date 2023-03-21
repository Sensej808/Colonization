using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    public CreateSelectionGrid grid;
    public GameObject reference;
    public static int lastpos;
    public void Start()
    {
        reference = transform.Find("Menu").Find("Reference").gameObject;
        Time.timeScale = 1;
        lastpos = -3;
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
            OpenCloseMenu();
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
    public void OpenCloseReference()
    {
        reference.SetActive(!reference.activeSelf);
    }
    public void OpenCloseMenu()
    {
        gameObject.transform.Find("Menu").gameObject.SetActive(!gameObject.transform.Find("Menu").gameObject.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gameObject.transform.Find("Menu").Find("Reference").gameObject.SetActive(false);
        }
        else
            Time.timeScale = 0;
    }
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void OpenLoseMenu()
    {
        gameObject.transform.Find("Lose").gameObject.SetActive(true);
    }
    public void AddMission(Mission mis)
    {
        GameObject m = Instantiate(Resources.Load<GameObject>("Prefabs/Mission"), transform.position, transform.rotation);
        m.transform.SetParent(gameObject.transform.Find("Missions"));
        m.GetComponent<RectTransform>().anchoredPosition = new Vector2(47, lastpos);
        m.GetComponent<RectTransform>().localScale = Vector2.one;
        m.transform.Find("Text").gameObject.GetComponent<Text>().text = mis.CompletedText;
        if (mis.FailedText != "")
        {
            m.transform.Find("Text").gameObject.GetComponent<Text>().text += "(" + mis.FailedText + ")";
        }
        m.name = mis.name;
        lastpos = lastpos-27;
    }
    public void CompleteMission(Mission m)
    {
        gameObject.transform.Find("Missions").Find(m.name).Find("Toggle").GetComponent<Toggle>().isOn = true;
        gameObject.transform.Find("Missions").Find(m.name).Find("Text").gameObject.GetComponent<Text>().color = new Color(132 / 255.0f, 129 / 255.0f, 129 / 255.0f);
    }
}
