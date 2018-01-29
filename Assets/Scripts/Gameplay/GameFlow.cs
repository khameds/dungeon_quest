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
        try
        {
            //Getting all the necessary parameters
            /*
            numberOfPlayer = System.Int32.Parse(LevelParam.Get("numberOfPlayer"));          // Disable as long as it's not finished
            */


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

        waveNum = 1;
        launchWave(waveNum);
    }

    private static void generatePlayers(int numberOfPlayer)
    {

        if (numberOfPlayer >= 1 && numberOfPlayer <= 4)
        {
            GameObject character = Instantiate(Resources.Load("Avatar/Character",typeof (GameObject))) as GameObject;

            Vector2 positionCharacter1 = GameObject.Find("PlayerSpawn1").transform.position;
            Vector2 positionCharacter2 = GameObject.Find("PlayerSpawn2").transform.position;
            Vector2 positionCharacter3 = GameObject.Find("PlayerSpawn3").transform.position;
            Vector2 positionCharacter4 = GameObject.Find("PlayerSpawn4").transform.position;

            character.transform.position = positionCharacter1;

            if (numberOfPlayer >= 2)
            {
                //Duplication
                GameObject character2 = Instantiate(character);
                //Move the object to the spaawn
                character2.transform.position = positionCharacter2;
            }
            if (numberOfPlayer >= 3)
            {
                GameObject character3 = Instantiate(character);
                character3.transform.position = positionCharacter3;
            }
            if (numberOfPlayer >= 4)
            {
                GameObject character4 = Instantiate(character);
                character4.transform.position = positionCharacter4;
            }
        }
        else
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
        //Time.timeScale = 0; //Pause the game
        //(We print Gameover with the choice to restart)

        //Back to the menu
        SceneManager.LoadScene("sandbox", LoadSceneMode.Single);
    }

    public static void noEnemy()
    {
        Debug.Log("[GameFlow.cs] Won Wave");

        launchWave(++waveNum);
    }

    private static void launchWave(int waveNum)
    {
        //Display the alert
        DisplayAlert.Print("Manche " + waveNum);

        GameObject enemy = Instantiate(Resources.Load("Avatar/MaceEnemy", typeof(GameObject))) as GameObject;
        enemy.SetActive(true);

        switch (waveNum)
        {
            case 1: //First wave
                GameObject enemy1_1 = Instantiate(enemy);
                GameObject enemy1_2 = Instantiate(enemy);
                GameObject enemy1_3 = Instantiate(enemy);
                enemy1_1.transform.position = GameObject.Find("EnemySpawn1").transform.position;
                enemy1_2.transform.position = GameObject.Find("EnemySpawn2").transform.position;
                enemy1_3.transform.position = GameObject.Find("EnemySpawn3").transform.position;
                break;
            case 2: //Second wave
                GameObject enemy2_1 = Instantiate(enemy);
                GameObject enemy2_2 = Instantiate(enemy);
                GameObject enemy2_3 = Instantiate(enemy);
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
        //We disable the example enemy
        enemy.SetActive(false);
    }

    private static void launchBossWave()
    {
        //TODO
    }
}
