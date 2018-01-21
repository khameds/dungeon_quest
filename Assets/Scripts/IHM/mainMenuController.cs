using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour {

    // Permit to acceed to the scene in paramater
	public void NewScene(string theScene)
    {
        SceneManager.LoadScene(theScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
