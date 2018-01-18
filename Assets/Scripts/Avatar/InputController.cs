using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{
    private PlayerController character;
    private bool jumping;


    private void Awake()
    {
        Debug.Log("Awake InputController!");
        character = GetComponent<PlayerController>();
        if (character == null)
            Debug.Log("Avatar non trouvé !");
    }


    private void Update()
    {
        if (!jumping)
        {
            // Read the jump input in Update so button presses aren't missed.
            jumping = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        character.Move(x, jumping);
        jumping = false;
    }
}