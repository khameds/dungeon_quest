﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiLocalController : MonoBehaviour
{
    public GameObject levelList;
    public GameObject hideDifficulty;
    private GameObject[] avatars;
    private GameObject[] levels;
    private int indexLevel;
    public Toggle difficultyNormal;
    public Toggle cooperationMode;
    public Text levelDescription;

    private void Start()
    {
        levels = new GameObject[levelList.transform.childCount];

        for (int i = 0; i < levelList.transform.childCount; i++)
        {
            levels[i] = levelList.transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in levels)
        {
            go.SetActive(false);
        }
        
    
        if (levels[0])
            levels[0].SetActive(true);
        levelDescription.text = "Niveau " + (indexLevel + 1).ToString();
    }
    

    public void ToggleLevel(int direction)
    {
        //Toggle off the current avatar
        levels[indexLevel].SetActive(false);

        indexLevel += direction;
        if (indexLevel < 0)
            indexLevel = levels.Length - 1;
        if (indexLevel >= levels.Length)
            indexLevel = 0;

        //Toggle on the new avatar
        levels[indexLevel].SetActive(true);
        levelDescription.text = "Niveau " + (indexLevel + 1).ToString();
    }

    public void goToNextScene()
    {
        if (cooperationMode.isOn)
        {
            LevelParam.Set("mode", "coop");
            if (difficultyNormal.isOn)
                LevelParam.Set("difficulty", "normal");
            else
                LevelParam.Set("difficulty", "hard");
        }
        else
            LevelParam.Set("mode", "versus");
        
        LevelParam.Set("levelNumber", indexLevel+1.ToString());
        int numberOfPlayer = 1 + GamepadManagement.GamepadConnectedNumber();
        LevelParam.Set("numberOfPlayer", ""+ numberOfPlayer);

        SceneManager.LoadScene("avatarSelection", LoadSceneMode.Additive);
    }

    public void backToPreviousScene()
    {
        SceneManager.LoadScene("mainMenu");
    }

    private void Update()
    {
        if (! cooperationMode.isOn)
            hideDifficulty.SetActive(false);
        else
            hideDifficulty.SetActive(true);

        if (Input.GetButtonDown("Cancel"))
            backToPreviousScene();
    }
}
