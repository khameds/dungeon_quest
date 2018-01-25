using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    int numberOfPlayer = 0;

    //Use this for initialization
    void Start ()
    {
        try
        {
            //Getting all the necessary parameters
                                                                                            /*
            numberOfPlayer = System.Int32.Parse(LevelParam.Get("numberOfPlayer"));          // Disable as long as it's not finished
            generatePlayers(numberOfPlayer);                                                */

            //Other parameters TODO
            // ...
        }
        catch (Exception e)
        {
            //Problem with a parameter
            Debug.Log("[GameFlow.cs] Problem with a level parameter");
            Debug.Log(e.Message);
            //Back to the menu
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }
        
	}

    private static void generatePlayers(int numberOfPlayer)
    {
        if(numberOfPlayer > 0 && numberOfPlayer < 4)
        {
            //TODO Instancier le nombre de joueur défini
        }
        else
        {
            //Back to the menu
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }
    }

    //Can't find any player
    public static void noPlayer()
    {
        Debug.LogError("[GameFlow.cs] GAMEOVER");

        //(We print Gameover with the choice to restart)

        //Back to the menu
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
}
