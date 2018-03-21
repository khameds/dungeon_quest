using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
    
    private bool displayPause = false;
    private bool displaySettings = false;
    public GameObject pauseObject;
    public GameObject settingsObject;

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (displayPause == true)
        {
            Time.timeScale = 0; //Stop the time in the game
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1; //Resume the game
            pauseObject.SetActive(false);
            settingsObject.SetActive(false);
        }

        if (displaySettings)
        {
            pauseObject.SetActive(false);
            settingsObject.SetActive(true);
        }
        else if (displayPause)
        {
            pauseObject.SetActive(true);
            settingsObject.SetActive(false);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (displaySettings)
            {
                displaySettings = false;
            }
            else
            {
                displayPause = !displayPause;
            }
        }

    }

    public void Resume()
    {
        displayPause = !displayPause;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    // Permit to acceed to the settings scene
    public void OpenSettings()
    {
        displaySettings = true;
    }

    //Exit the Options scene to come back to the previous scene (main menu or pause menu)
    public void ExitSettings()
    {
        displaySettings = false;
    }
}
