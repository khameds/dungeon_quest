using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSuccessMenuController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void NextLevel()
    {
        if (LevelParam.Get("levelNumber") == null)
            LevelParam.Set("levelNumber", "1");

        if (System.Int32.Parse(LevelParam.Get("levelNumber")) < 2)
            LevelParam.Set("levelNumber", System.Int32.Parse(LevelParam.Get("levelNumber")) + 1 + "");
        SceneManager.LoadScene("sandbox", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
}
