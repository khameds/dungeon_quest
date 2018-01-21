using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public GameObject menuObject;
    private bool isActive = false; 
	
	// Update is called once per frame
	void Update ()
    {
		if (isActive == true)
        {
            menuObject.SetActive(true);
            
            //TODO 
            //We must see the cursor
            //Cursor.visible = true;
            
            //We stop the game by stopping the time
            Time.timeScale = 0;
        }
        else
        {
            menuObject.SetActive(false);

            //TODO
            //We must see the cursor
            //Cursor.visible = false;

            //We resume the game
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
        }
	}

    public void Resume()
    {
        isActive = !isActive;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    // Permit to acceed to the settings scene
    public void OpenSettings()
    {
        SceneManager.LoadScene("options", LoadSceneMode.Additive);
    }
}
