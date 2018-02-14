using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSuccessMenuController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("sandbox", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
}
