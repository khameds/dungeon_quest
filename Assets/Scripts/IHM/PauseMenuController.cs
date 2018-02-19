using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
    
    private bool displaySettings = false;
    public GameObject pauseObject;

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (displaySettings == true)
        {
            Time.timeScale = 0; //Stop the time in the game
            pauseObject.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1; //Resume the game
            pauseObject.SetActive(false);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            displaySettings = !displaySettings;
        }

    }

    public void Resume()
    {
        displaySettings = !displaySettings;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    // Permit to acceed to the settings scene
    public void OpenSettings()
    {
        SceneManager.LoadScene("options", LoadSceneMode.Additive);
    }
}
