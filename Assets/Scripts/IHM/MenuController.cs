using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    
    private bool displaySettings = false;
    public GameObject settingsObject;

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        settingsObject.SetActive(displaySettings);

        if (displaySettings && Input.GetButtonDown("Cancel"))
        {
            displaySettings = false;
        }
    }

    // Permit to acceed to the scene in paramater
    public void NewScene(string theScene)
    {
        SceneManager.LoadScene(theScene, LoadSceneMode.Single);

    }

    // Permit to acceed to the settings scene
    public void OpenSettings()
    {
        displaySettings = true;
    }

    public void ExitSettings()
    {
        displaySettings = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
