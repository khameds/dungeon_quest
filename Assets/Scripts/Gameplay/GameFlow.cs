using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    private static int waveNum;
    private static int roundNum;
    private static int numberOfPlayer = 1;
    private static int levelNumber = 1;
    private static String mode = "coop";
    public static int alivePlayers;
    public AstarPath path;

    //Use this for initialization
    void Start ()
    {
        //Getting all the necessary parameters
        try
        {
            if(LevelParam.Get("numberOfPlayer")!=null)
                numberOfPlayer = System.Int32.Parse(LevelParam.Get("numberOfPlayer"));

            if (LevelParam.Get("levelNumber") != null)
                levelNumber = System.Int32.Parse(LevelParam.Get("levelNumber"));
            else
                LevelParam.Set("levelNumber", "1");

            if (LevelParam.Get("mode") != null) //"coop" or "versus"
                mode = LevelParam.Get("mode");
            else
                LevelParam.Set("mode", "coop");
        }
        catch (Exception e)
        {
            //Problem with a parameter
            Debug.Log("[GameFlow.cs] Problem with a level parameter" + e);
            Debug.Log(e.Message);
            //Back to the menu
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }

        //Loading of the level
        GameObject level = Instantiate(Resources.Load("Level/Level"+levelNumber, typeof(GameObject))) as GameObject;

        //Loading of the players
        generatePlayers(numberOfPlayer);

        if(mode.Equals("coop"))
        {
            //Launching the first wave
            waveNum = 1;
            launchWave();
        }
        else
        {
            //Launching the first round
            roundNum = 1;
            launchRound();
        }
    }

    private void FixedUpdate()
    {
        //Verification about the death
        playerVerification();
        if (mode.Equals("coop"))
        {
            //Verification about the death
            mobVerification();
        }
    }

    private void generatePlayers(int numberOfPlayer)
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

            character.GetComponent<SpriteRenderer>().color = getColorFromParam(1);

            GameObject character1 = Instantiate(character);
            character1.transform.position = positionCharacter1;
            character1.GetComponent<UserControl>().setNumber(0);
            character1.GetComponent<SightingSystem>().setNumber(0);
            character1.GetComponent<ShootingSystem>().setNumber(0);

            if (numberOfPlayer >= 2) //Player 2
            {
                character.GetComponent<SpriteRenderer>().color = getColorFromParam(2);
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
                character.GetComponent<SpriteRenderer>().color = getColorFromParam(3);
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
                character.GetComponent<SpriteRenderer>().color = getColorFromParam(4);
                //Duplication
                GameObject character4 = Instantiate(character);
                //Move the object to the spawn spot
                character4.transform.position = positionCharacter4;
                character4.GetComponent<UserControl>().setNumber(3);
                character4.GetComponent<SightingSystem>().setNumber(3);
                character4.GetComponent<ShootingSystem>().setNumber(3);
                
            }
            character.SetActive(false);
        }
        else //Problem
        {
            Debug.Log("[GameFlow.cs] Problem with numberOfPlayer's value");
            //Back to the menu
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }
    }

    //Can find only one player (versus mode)
    public void onePlayerOnly(GameObject player)
    {
        int playerNumber = player.GetComponent<UserControl>().getNumber() + 1;
        Debug.Log("[GameFlow.cs] The player "+playerNumber+" won !");


        Debug.Log("scoreP" + playerNumber + " = "+ LevelParam.Get("scoreP" + playerNumber));


        int actualScore = System.Int32.Parse(LevelParam.Get("scoreP" + playerNumber));
        //We add a point to this player
        LevelParam.Set("scoreP" + playerNumber, actualScore + 1 + "");

        launchRound();
    }

    private void playerVerification()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        ArrayList alivePlayersList = new ArrayList();
        alivePlayers = 0;

        foreach (GameObject player in players)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (!playerHealth.isDead)
            {
                alivePlayers++;
                alivePlayersList.Add(player);
            }
        }

        Debug.Log("alivePlayers = " + alivePlayers);
        
        if (LevelParam.Get("mode").Equals("coop") && alivePlayers == 0)
        {
            noPlayer();
        }
        if (LevelParam.Get("mode").Equals("versus") && alivePlayers == 1)
        {
            onePlayerOnly((GameObject)alivePlayersList.ToArray()[0]);
        }
    }

    public Color getColorFromParam(int indexPlayer)
    {
        String avatar = "color" + indexPlayer;
        switch (LevelParam.Get(avatar))
        {
            case "0":
                return new Color(1, 1, 0);
            case "1":
                return new Color(1, 0, 1);
            case "2":
                return new Color(0, 1, 1);
            case "3":
                return new Color(1, 0.5f, 0);
            case "4":
                return new Color(0, 0, 1);
            default:
                return new Color(1, 1, 1);
        }
    }

    private void launchRound()
    {
        switch (roundNum)
        {
            case 1: //First wave

                //Reset/Initialization of the score
                LevelParam.Set("scoreP1", "0");
                LevelParam.Set("scoreP2", "0");
                LevelParam.Set("scoreP3", "0");
                LevelParam.Set("scoreP4", "0");

                //Display the alert on the game
                DisplayAlert.Print("Manche " + roundNum);
                roundNum++;
                break;
            case 2: //Second wave
                resetHealth();
                resetPosition();

                //Display the alert on the game
                DisplayAlert.Print("Manche " + roundNum);
                roundNum++;
                break;
            case 3: //Third wave
                resetHealth();
                resetPosition();

                //Display the alert on the game
                DisplayAlert.Print("Manche " + roundNum);
                roundNum++;
                break;
            case 4: //Won
                SceneManager.LoadScene("versusLevelSuccess", LoadSceneMode.Single);
                break;
        }
    }

    private void resetPosition()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            int playerNumber = player.GetComponent<UserControl>().getNumber() + 1;
            String spawnName = "PlayerSpawn" + playerNumber;
            player.transform.position = GameObject.Find(spawnName).transform.position;
        }
    }

    private void resetHealth()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.FullRevive();
        }
    }

    ////////// Coop only part /*///////


    private void mobVerification()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyAlive = enemies.Length;

        if (enemyAlive == 0)
        {
            noEnemy();
        }
    }

    //Can't find any player
    public void noPlayer()
    {
        //Launch a choice menu to restart/quit
        if (!SceneManager.GetSceneByName("gameOver").isLoaded)
        {
            Debug.Log("[GameFlow.cs] GAMEOVER");
            SceneManager.LoadScene("gameOver", LoadSceneMode.Additive);
        }
    }

    //Can't find any enemy
    public void noEnemy()
    {
        if (LevelParam.Get("mode").Equals("coop"))
        {
            Debug.Log("[GameFlow.cs] Won Wave");
            launchWave();
        }
    }

    private void launchWave()
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
                path.Scan();
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
                path.Scan();
                resetHealth();
                //Move the objects to the spawn 
                enemy2_1.transform.position = GameObject.Find("EnemySpawn1").transform.position;
                enemy2_2.transform.position = GameObject.Find("EnemySpawn2").transform.position;
                enemy2_3.transform.position = GameObject.Find("EnemySpawn3").transform.position;
                waveNum++;
                break;
            case 3: //Boss wave

                //Display the alert on the game
                DisplayAlert.Print("Boss");

                resetHealth();
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

    private void launchBossWave()
    {
        
        //Loading and instatation of the prefab of the boss
        GameObject boss = Instantiate(Resources.Load("Avatar/Boss", typeof(GameObject))) as GameObject;

        //Move the boss to the spawn
        boss.transform.position = GameObject.Find("EnemySpawn1").transform.position;
        
        path.Scan(); 
        
    }
}
