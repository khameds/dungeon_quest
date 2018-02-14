using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    void Start()
    {
        Cursor.visible = true;
    }

    // Permit to acceed to the scene in paramater
    public void NewGame(string theScene)
    {


        SceneManager.LoadScene(theScene, LoadSceneMode.Single);

    }

    // Permit to acceed to the settings scene
    public void OpenSettings()
    {
        SceneManager.LoadScene("options", LoadSceneMode.Additive);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
