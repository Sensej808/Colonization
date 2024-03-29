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
    public int lastpos;
    public List<GameObject> listImageSelectedUnits;
    public GameObject SelectedBuilding;
    public int LastOrderedUnit;
    public List<GameObject> listorderedUnit;
    public void Start()
    {
        reference = transform.Find("Menu").Find("Reference").gameObject;
        Time.timeScale = 1;
        //lastpos = 0;
    }
    public void Update()
    {
        //���� �� �������� ������, ��������� ������������� ����������� ���, � � ���� ������ ������
        //�� ������, ������� �������� ��������, ���� 0, �� �� ��������� buildmenu
        //���� �� 0, �� ���������
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
        print(lastpos - (int)m.transform.Find("Text").GetComponent<RectTransform>().rect.height);
        lastpos -=(int)m.transform.Find("Text").GetComponent<RectTransform>().rect.height;
    }
    public void CompleteMission(Mission m)
    {
        gameObject.transform.Find("Missions").Find(m.name).Find("Toggle").GetComponent<Toggle>().isOn = true;
        gameObject.transform.Find("Missions").Find(m.name).Find("Text").gameObject.GetComponent<Text>().color = new Color(132 / 255.0f, 129 / 255.0f, 129 / 255.0f);
    }
    public void DrawSelectedUnit(List<GameObject> l)
    {
        foreach (GameObject go in listImageSelectedUnits)
            Destroy(go);
        Vector2 firstPos = new Vector2(-190, 17);
        int i = 0;
        int k = 0;
        foreach(GameObject go in l)
        {
            if (i > 29)
                break;
            if (k == 10)
            {
                firstPos.y -= 23;
                firstPos.x = -190;
                k = 0;
            }
            GameObject m = Instantiate(go.GetComponent<Health>().icon, transform.position, transform.rotation);
            m.transform.SetParent(gameObject.transform.Find("PanelSelectedUnit"));
            m.GetComponent<RectTransform>().anchoredPosition = firstPos;
            m.GetComponent<RectTransform>().localScale = Vector2.one;
            firstPos.x += 23;
            listImageSelectedUnits.Add(m);
            m.GetComponent<Icon>().health = go.GetComponent<Health>();
            go.GetComponent<Health>().onGetDamage += m.GetComponent<Icon>().ChangeColor;
            go.GetComponent<Health>().onGetHealth += m.GetComponent<Icon>().ChangeColor;
            i++;
            k++;
        }
    }
    public void OpenOrderedUnitPanel()
    {
        gameObject.transform.Find("PanelOrderedUnits").gameObject.SetActive(true);
        SelectedBuilding = Storage.selectedUnits[0];
        LastOrderedUnit = 0;
        foreach(var x in SelectedBuilding.GetComponent<DoUnits>().queueUnits)
            AddUnit(x);
    }
    public void CloseOrderedUnitPanel()
    {
        foreach (var x in listorderedUnit)
            Destroy(x);
        listorderedUnit.Clear();
        gameObject.transform.Find("PanelOrderedUnits").gameObject.SetActive(false);
        SelectedBuilding = null;
        LastOrderedUnit = 0;
    }
    public void AddUnit(GameObject unit)
    {
        LastOrderedUnit++;
        GameObject x = Instantiate(unit.GetComponent<Health>().OrderIcon, transform.Find("PanelOrderedUnits").Find(LastOrderedUnit.ToString())).gameObject;
        listorderedUnit.Add(x);
    }   
    public void RemoveUnit()
    {
        LastOrderedUnit = 0;
        foreach (var x in listorderedUnit)
            Destroy(x);
        foreach (var x in SelectedBuilding.GetComponent<DoUnits>().queueUnits)
            AddUnit(x);
    }
}
