using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    private static int waveNum;
    private static int numberOfPlayer = 2;
    private static int levelNumber = 1;
    public static int alivePlayers;

    //Use this for initialization
    void Start ()
    {
        //Getting all the necessary parameters

        try
        {
            if(LevelParam.Get("numberOfPlayer")!=null)
                numberOfPlayer = System.Int32.Parse(LevelParam.Get("numberOfPlayer"));

            if (LevelParam.Get("levelNumber") != null)
                numberOfPlayer = System.Int32.Parse(LevelParam.Get("numberOfPlayer"));


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


        GameObject level = Instantiate(Resources.Load("Level/Level1", typeof(GameObject))) as GameObject;

        generatePlayers(numberOfPlayer);

        //Launching the first wave

        waveNum = 1;
        launchWave();
    }

    private void FixedUpdate()
    {
        playerVerification();
        mobVerification();
    }

    private void playerVerification()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        alivePlayers = 0;

        foreach (GameObject player in players)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if(playerHealth.currentHealth!=0)
            {
                alivePlayers++;
            }
        }

        if(alivePlayers == 0)
        {
            noPlayer();
        }
    }

    private void mobVerification()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyAlive = enemies.Length;

        if (enemyAlive == 0)
        {
            noEnemy();
        }
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
            character.GetComponent<UserControl>().setNumber(0);
            character.GetComponent<SightingSystem>().setNumber(0);
            character.GetComponent<ShootingSystem>().setNumber(0);

            if (numberOfPlayer >= 2) //Player 2
            {
                //Duplication
                GameObject character2 = Instantiate(character);
                //Move the object to the spawn spot
                character2.transform.position = positionCharacter2;
                character2.GetComponent<UserControl>().setNumber(1);
                character2.GetComponent<SightingSystem>().setNumber(1);
                character2.GetComponent<ShootingSystem>().setNumber(1);


            }
            if (numberOfPlayer >= 3) //Player 3
            {
                //Duplication
                GameObject character3 = Instantiate(character);
                //Move the object to the spawn spot
                character3.transform.position = positionCharacter3;
                character3.GetComponent<UserControl>().setNumber(2);
                character3.GetComponent<SightingSystem>().setNumber(2);
                character3.GetComponent<ShootingSystem>().setNumber(2);


            }
            if (numberOfPlayer >= 4) //Player 4
            {
                //Duplication
                GameObject character4 = Instantiate(character);
                //Move the object to the spawn spot
                character4.transform.position = positionCharacter4;
                character4.GetComponent<UserControl>().setNumber(3);
                character4.GetComponent<SightingSystem>().setNumber(3);
                character4.GetComponent<ShootingSystem>().setNumber(3);
                
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
        //Launch a choice menu to restart/quit (future version)
        /*if(!SceneManager.GetSceneByName("gameOver").isLoaded)
        {
            Debug.Log("[GameFlow.cs] GAMEOVER");
            SceneManager.LoadScene("gameOver", LoadSceneMode.Additive);
        }*/
    }

    //Can't find any enemy
    public static void noEnemy()
    {
        Debug.Log("[GameFlow.cs] Won Wave");

        launchWave();
    }

    private static void launchWave()
    {
        //Loading and instatation of the prefab of enemy
        GameObject enemy = Instantiate(Resources.Load("Avatar/MaceEnemy", typeof(GameObject))) as GameObject;
        enemy.SetActive(true);

        switch (waveNum)
        {
            case 1: //First wave

                //Display the alert on the game
                DisplayAlert.Print("Manche " + waveNum);

                //Duplication
                GameObject enemy1_1 = Instantiate(enemy);
                GameObject enemy1_2 = Instantiate(enemy);
                GameObject enemy1_3 = Instantiate(enemy);
                //Move the objects to the spawn spot
                enemy1_1.transform.position = GameObject.Find("EnemySpawn1").transform.position;
                enemy1_2.transform.position = GameObject.Find("EnemySpawn2").transform.position;
                enemy1_3.transform.position = GameObject.Find("EnemySpawn3").transform.position;
                waveNum++;
                break;
            case 2: //Second wave

                //Display the alert on the game
                DisplayAlert.Print("Manche " + waveNum);

                //Duplication
                GameObject enemy2_1 = Instantiate(enemy);
                GameObject enemy2_2 = Instantiate(enemy);
                GameObject enemy2_3 = Instantiate(enemy);
                //Move the objects to the spawn 
                enemy2_1.transform.position = GameObject.Find("EnemySpawn1").transform.position;
                enemy2_2.transform.position = GameObject.Find("EnemySpawn2").transform.position;
                enemy2_3.transform.position = GameObject.Find("EnemySpawn3").transform.position;
                waveNum++;
                break;
            case 3: //Boss wave

                //Display the alert on the game
                DisplayAlert.Print("Boss");

                launchBossWave();
                waveNum++;
                break; 
            case 4: //Won
                Debug.Log("[GameFlow.cs] WON !");
                SceneManager.LoadScene("levelSuccess", LoadSceneMode.Single);
                break;
        }

        //We disable the instantiated enemy
        enemy.SetActive(false);
    }

    private static void launchBossWave()
    {
        /*
        //Loading and instatation of the prefab of the boss
        GameObject boss = Instantiate(Resources.Load("Avatar/Boss", typeof(GameObject))) as GameObject;

        //Move the boss to the spawn
        boss.transform.position = GameObject.Find("EnemySpawn1").transform.position;
        */
    }
}
