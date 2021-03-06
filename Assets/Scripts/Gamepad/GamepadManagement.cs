﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamepadManagement : MonoBehaviour
{
    public static GamePadState[] state = new GamePadState[4];
    public static GamePadState[] prevState = new GamePadState[4];

    void Start ()
    {
		
	}

    private void FixedUpdate()
    {

        //GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
    }

    public static int GamepadConnectedNumber()
    {
        GamePadState test1 = GamePad.GetState((PlayerIndex)0);
        GamePadState test2 = GamePad.GetState((PlayerIndex)1);
        GamePadState test3 = GamePad.GetState((PlayerIndex)2);
        GamePadState test4 = GamePad.GetState((PlayerIndex)3);

        int count = 0;

        if (test1.IsConnected)
            count++;
        if (test2.IsConnected)
            count++;
        if (test3.IsConnected)
            count++;
        if (test4.IsConnected)
            count++;

        return count;
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(GamepadConnectedNumber());

        //Detecting gamepads
        if(prevState.Length.Equals(0) || !prevState[0].IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("{0} gamepad(s) found ", testPlayerIndex));
                }
                state[i] = GamePad.GetState(testPlayerIndex);
            }
        }

        //Update gamepads
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            prevState[i] = state[i];
            state[i] = GamePad.GetState(testPlayerIndex);
        }
        

        /*
        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
        */
    }

    public static GamePadState getPrevStateByUserNumber(int userNumber)
    {
        switch (userNumber)
        {
            case 0:
                Debug.Log("ERROR : Player 1 don't have any gamepad");
                throw new System.RankException();
            case 1:
                return prevState[0];
            case 2:
                return prevState[1];
            case 3:
                return prevState[2];
            default:
                Debug.Log("ERROR : Invalid userNumber");
                throw new System.RankException();
        }
    }

    public static GamePadState getStateByUserNumber(int userNumber)
    {
        switch(userNumber)
        {
            case 0:
                Debug.Log("ERROR : Player 1 don't have any gamepad");
                throw new System.RankException();
            case 1:
                return state[0];
            case 2:
                return state[1];
            case 3:
                return state[2];
            default:
                Debug.Log("ERROR : Invalid userNumber");
                throw new System.RankException();
        }
    }
}
