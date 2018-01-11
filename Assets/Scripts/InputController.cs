using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{
    private PlayerController m_Character;
    private bool m_Jump;


    private void Awake()
    {
        Debug.Log("Awake InputController!");
        m_Character = GetComponent<PlayerController>();
        if (m_Character == null)
            Debug.Log("Avatar non trouvé !");
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;
    }
}