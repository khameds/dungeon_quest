using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SightingSystem : MonoBehaviour
{
    /*
    public Rigidbody2D sight;
    public Rigidbody2D character;
    private Vector2 direction;

    private Vector2 oldMousePosition;
    private Vector2 oldDirection;
    private float sensibility = 2f;

    // Property for weapon use
    public Vector2 Direction
    {
        get { return direction; }
    }



    public float maxDragDistance = 2f; //We can set it by the type of the selected weapon
    void Awake()
    {
        character = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = false;
        oldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        oldDirection = (oldMousePosition - character.position).normalized;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //direction = (mousePos - character.position).normalized;
        direction = (oldDirection + (mousePos - oldMousePosition).normalized).normalized;
        if (Vector3.Distance(mousePos, character.position) > maxDragDistance)
            sight.position = character.position + direction * maxDragDistance;
        // else
        //   sight.position = mousePos;

        //https://docs.unity3d.com/ScriptReference/Vector2.SmoothDamp.html
        //   if (Vector2.Scale(mousePos - oldMousePosition) > sensibility)
        {
            oldMousePosition = mousePos;
            oldDirection = (mousePos - character.position).normalized;
        }
    }

    void OnMouseDown()
    {

        //Switch on weapon selected and FIREEEEEE if enough ammo

    }

    void OnMouseUp()
    {
        //For weapons that require letting the key press
    }

    */
}
