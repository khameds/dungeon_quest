using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class UserControl : MonoBehaviour
{
    private PlayerController player;
    private bool wantToJump;


    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (!wantToJump)
        {
            // Read the jump input in Update so button presses aren't missed.
            wantToJump = Input.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {

        // Pass all parameters to the character control script.
        player.Move(Input.GetAxis("Horizontal"), wantToJump);
        wantToJump = false;
    }
}