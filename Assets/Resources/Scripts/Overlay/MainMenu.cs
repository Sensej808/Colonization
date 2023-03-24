using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject reference;
    public void Start()
    {
        reference = GameObject.Find("Canvas");
        reference = reference.transform.Find("Reference").gameObject;
    }
    public void OpenCloseReference()
    {
        reference.SetActive(!reference.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();

    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("LevelComplete", 0);
        SceneManager.LoadScene("LevelSelection");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
