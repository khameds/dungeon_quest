using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoloGameController : MonoBehaviour
{
    public GameObject avatarList;
    public GameObject levelList;
    private GameObject[] avatars;
    private GameObject[] levels;
    private int indexAvatar;
    private int indexLevel;
    public Toggle difficultyNormal;
    public Text levelDescription;

    private void Start()
    {
        avatars = new GameObject[avatarList.transform.childCount];
        levels = new GameObject[levelList.transform.childCount];

        //Fill the array
        for(int i=0; i< avatarList.transform.childCount; i++)
        {
            avatars[i] = avatarList.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < levelList.transform.childCount; i++)
        {
            levels[i] = levelList.transform.GetChild(i).gameObject;
        }

        //Toggle off the renderer
        foreach (GameObject go in avatars)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in levels)
        {
            go.SetActive(false);
        }
        
        //Toggle the first index
        if (avatars[0])
            avatars[0].SetActive(true);
        if (levels[0])
            levels[0].SetActive(true);
        levelDescription.text = "Niveau " + (indexLevel + 1).ToString();
    }

    public void ToggleAvatar(int direction)
    {
        //Toggle off the current avatar
        avatars[indexAvatar].SetActive(false);

        indexAvatar += direction;
        if (indexAvatar < 0)
            indexAvatar = avatars.Length - 1;
        if (indexAvatar >= avatars.Length)
            indexAvatar = 0;

        //Toggle on the new avatar
        avatars[indexAvatar].SetActive(true);
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
        levelDescription.text = "Niveau " + (indexLevel+1).ToString();
    }

    public void createGame()
    {
        LevelParam.Set("color1", indexAvatar.ToString());
        if (difficultyNormal.isOn)
            LevelParam.Set("difficulty", "normal");
        else
            LevelParam.Set("difficulty", "hard");
        
        LevelParam.Set("levelNumber", (indexLevel+1).ToString());

        SceneManager.LoadScene("sandbox");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
