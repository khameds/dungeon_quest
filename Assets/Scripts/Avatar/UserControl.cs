using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class UserControl : MonoBehaviour
{
    private PlayerController player;
    private PlayerHealth playerHealth;
    private bool wantToJump;
    private bool isDead;

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
            // Read the jump input in Update so button presses aren't missed.
            wantToJump = Input.GetKeyDown(GameInputManager.GIM.jump);
        }
    }


    private void FixedUpdate()
    {

        // Pass all parameters to the character control script.
        if (!Dead)
        {
            int direction = 0;
            if (Input.GetKey(GameInputManager.GIM.left))
                direction--;
            if (Input.GetKey(GameInputManager.GIM.right))
                direction++;

            player.Move(direction, wantToJump);
            wantToJump = false;
        }
    }
}