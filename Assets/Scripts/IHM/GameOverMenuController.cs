using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("sandbox", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
}
