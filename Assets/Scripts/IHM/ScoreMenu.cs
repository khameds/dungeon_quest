using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    void Start()
    {
        //Getting
        String scoreP1 = "(Not found)";
        String scoreP2 = "(Not found)";
        String scoreP3 = "(Not found)";
        String scoreP4 = "(Not found)";

        if (LevelParam.Get("scoreP1")!=null && LevelParam.Get("scoreP2") != null && LevelParam.Get("scoreP3") != null && LevelParam.Get("scoreP4") != null)
        {
            scoreP1 = LevelParam.Get("scoreP1");
            scoreP2 = LevelParam.Get("scoreP2");
            scoreP3 = LevelParam.Get("scoreP3");
            scoreP4 = LevelParam.Get("scoreP4");
        }

        //Displaying
        Text textObject = this.gameObject.GetComponent<Text>();
        textObject.text = ""+
            "Player 1 - "+ scoreP1 + " points\n" +
            "Player 2 - "+ scoreP2 + " points\n" +
            "Player 3 - "+ scoreP3 + " points\n" +
            "Player 4 - "+ scoreP4 + " points";
    }
}
