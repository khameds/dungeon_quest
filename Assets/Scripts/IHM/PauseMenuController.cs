using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
    
    private bool displaySettings = false;

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            //If the game is not in pause
            if (! SceneManager.GetSceneByName("pause").isLoaded)
            {
                //We have to set the game in pause by loading the pause scene and stopping the game
                Time.timeScale = 0; //Stop the time in the game

                //We load the pause scene
                SceneManager.LoadScene("pause", LoadSceneMode.Additive);
            }
            else
            {
                Resume();
            }
        }

	}

    public void Resume()
    {
        SceneManager.UnloadSceneAsync("pause");
        Cursor.visible = false;
        Time.timeScale = 1;
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
