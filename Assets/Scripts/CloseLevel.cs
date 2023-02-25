using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseLevel : MonoBehaviour
{
    public void ExitChangeMenu()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
