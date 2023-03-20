using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public int numberScene;
    public static WinScript Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            PlayerPrefs.SetInt("LevelComplete", numberScene);
            SceneManager.LoadScene("LevelSelection");
        }
        */
    }
    public void CheatingWin()
    {
        PlayerPrefs.SetInt("LevelComplete", numberScene);
        print(PlayerPrefs.GetInt("LevelComplete"));
        SceneManager.LoadScene("LevelSelection");
    }
    public void Lose()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").GetComponent<OpenMenu>().OpenLoseMenu();
    }
}
