using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    public int numberScene;
    private void Start()
    {
        if (numberScene <= PlayerPrefs.GetInt("LevelComplete"))
            gameObject.transform.Find("flag1").gameObject.SetActive(true);
        if (numberScene == PlayerPrefs.GetInt("LevelComplete") + 1)
            gameObject.transform.Find("flag2").gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("LevelComplete") == 15)
        {

        }
    }
    private void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("LevelComplete") + 1 == numberScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            //print(PlayerPrefs.GetInt("LevelComplete") + 1);
        }
    }
}
