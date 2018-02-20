using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(PlayerController))]
public class UserControl : MonoBehaviour
{
    private PlayerController player;
    private PlayerHealth playerHealth;
    private bool wantToJump;
    private bool isDead;
    public int userNumber;

    public bool Dead
    {
        get{ return playerHealth.isDead;}
        set{ playerHealth.isDead = value; }
    }
    

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>(); 
    }


    private void Update()
    {
        if (!wantToJump && !Dead)
        {
            //Keyboard
            // Read the jump input in Update so button presses aren't missed.
            if (userNumber == 0)
            {
                wantToJump = Input.GetKeyDown(GameInputManager.GIM.jump);
            }

            //Gamepad
            wantToJump = (GamepadManagement.prevState[userNumber].Triggers.Left == 1/* && GamepadManagement.state[userNumber].Buttons.LeftShoulder == ButtonState.Released*/);
        }
    }


    private void FixedUpdate()
    {

        // Pass all parameters to the character control script.
        if (!Dead)
        {
            moveManagement();

            wantToJump = false;
        }
    }

    private void moveManagement()
    {
        GameInputManager.direction = 0;

        //Keyboard only for player 1
        if (userNumber==0)
        {
            if (Input.GetKey(GameInputManager.GIM.left))
                GameInputManager.direction = -1;
            if (Input.GetKey(GameInputManager.GIM.right))
                GameInputManager.direction = 1;
        }

        //DPad or Joystick choice
        if (GamepadManagement.state[userNumber].ThumbSticks.Left.X == 0)
        {
            //DPad
            if (GamepadManagement.state[userNumber].DPad.Left == ButtonState.Pressed)
            {
                GameInputManager.direction = -1;
            }

            if (GamepadManagement.state[userNumber].DPad.Right == ButtonState.Pressed)
            {
                GameInputManager.direction = 1;
            }

            player.Move(GameInputManager.direction, wantToJump);
        }
        else
        {
            //Joystick
            player.Move(GamepadManagement.state[userNumber].ThumbSticks.Left.X, wantToJump);
        }
    }

    //Set the number of this player (0 => 3)
    internal void setNumber(int v)
    {
        userNumber = v;
    }
}