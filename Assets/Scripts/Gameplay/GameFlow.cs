using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    private static int waveNum;
    private static int numberOfPlayer = 1;

    //Use this for initialization
    void Start ()
    {
        //Getting all the necessary parameters

        try
        {      
            //numberOfPlayer = System.Int32.Parse(LevelParam.Get("numberOfPlayer"));
            
            //Other parameters TODO
            // ...
        }
        catch (Exception e)
        {
            //Problem with a parameter
            Debug.Log("[GameFlow.cs] Problem with a level parameter" + e);
            Debug.Log(e.Message);
            //Back to the menu
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }

        generatePlayers(numberOfPlayer);

        //Launching the first wave
        waveNum = 1;
        launchWave(waveNum);
    }

    private static void generatePlayers(int numberOfPlayer)
    {
        //Generating the correct number of player on the spawn spot fixed on the map
        if (numberOfPlayer >= 1 && numberOfPlayer <= 4)
        {
            //Loading the prefab Character
            GameObject character = Instantiate(Resources.Load("Avatar/Character",typeof (GameObject))) as GameObject;

            //Loading the spawn spots
            Vector2 positionCharacter1 = GameObject.Find("PlayerSpawn1").transform.position;
            Vector2 positionCharacter2 = GameObject.Find("PlayerSpawn2").transform.position;
            Vector2 positionCharacter3 = GameObject.Find("PlayerSpawn3").transform.position;
            Vector2 positionCharacter4 = GameObject.Find("PlayerSpawn4").transform.position;

            //Player 1
            character.transform.position = positionCharacter1;

            if (numberOfPlayer >= 2) //Player 2
            {
                //Duplication
                GameObject character2 = Instantiate(character);
                //Move the object to the spawn spot
                character2.transform.position = positionCharacter2;
            }
            if (numberOfPlayer >= 3) //Player 3
            {
                //Duplication
                GameObject character3 = Instantiate(character);
                //Move the object to the spawn spot
                character3.transform.position = positionCharacter3;
            }
            if (numberOfPlayer >= 4) //Player 4
            {
                //Duplication
                GameObject character4 = Instantiate(character);
                //Move the object to the spawn spot
                character4.transform.position = positionCharacter4;
            }
        }
        else //Problem
        {
            Debug.Log("[GameFlow.cs] Problem with numberOfPlayer's value");
            //Back to the menu
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }
    }

    //Can't find any player
    public static void noPlayer()
    {
        Debug.Log("[GameFlow.cs] GAMEOVER");

        //Launch a choice menu to restart/quit (future version)
        //SceneManager.LoadScene("gameOver", LoadSceneMode.Additive);

        //Back to the menu (actual version)
        SceneManager.LoadScene("sandbox", LoadSceneMode.Single);
    }

    //Can't find any enemy
    public static void noEnemy()
    {
        Debug.Log("[GameFlow.cs] Won Wave");

        launchWave(++waveNum);
    }

    private static void launchWave(int waveNum)
    {
        //Display the alert on the game
        DisplayAlert.Print("Manche " + waveNum);

        //Loading and instatation of the prefab of enemy
        GameObject enemy = Instantiate(Resources.Load("Avatar/MaceEnemy", typeof(GameObject))) as GameObject;
        enemy.SetActive(true);

        switch (waveNum)
        {
            case 1: //First wave
                //Duplication
                GameObject enemy1_1 = Instantiate(enemy);
                GameObject enemy1_2 = Instantiate(enemy);
                GameObject enemy1_3 = Instantiate(enemy);
                //Move the objects to the spawn spot
                enemy1_1.transform.position = GameObject.Find("EnemySpawn1").transform.position;
                enemy1_2.transform.position = GameObject.Find("EnemySpawn2").transform.position;
                enemy1_3.transform.position = GameObject.Find("EnemySpawn3").transform.position;
                break;
            case 2: //Second wave
                //Duplication
                GameObject enemy2_1 = Instantiate(enemy);
                GameObject enemy2_2 = Instantiate(enemy);
                GameObject enemy2_3 = Instantiate(enemy);
                //Move the objects to the spawn 
                enemy2_1.transform.position = GameObject.Find("EnemySpawn1").transform.position;
                enemy2_2.transform.position = GameObject.Find("EnemySpawn2").transform.position;
                enemy2_3.transform.position = GameObject.Find("EnemySpawn3").transform.position;
                break;
            case 3: //Boss wave
                launchBossWave();
                break;
            case 4: //Won
                Debug.Log("[GameFlow.cs] WON !");
                SceneManager.LoadScene("menu", LoadSceneMode.Single);
                break;
        }

        //We disable the instantiated enemy
        enemy.SetActive(false);
    }

    private static void launchBossWave()
    {
        //TODO
    }
}
