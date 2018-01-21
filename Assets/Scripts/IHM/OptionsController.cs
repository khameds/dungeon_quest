using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

    // Permit to acceed to the scene in paramater
    public void ExitSettings()
    {
        SceneManager.UnloadSceneAsync("options");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
