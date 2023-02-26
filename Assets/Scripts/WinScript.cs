using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public int numberScene;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            PlayerPrefs.SetInt("LevelComplete", numberScene);
            SceneManager.LoadScene("LevelSelection");
        }
    }
    public void CheatingWin()
    {
        PlayerPrefs.SetInt("LevelComplete", numberScene);
        print(PlayerPrefs.GetInt("LevelComplete"));
        SceneManager.LoadScene("LevelSelection");
    }
}
