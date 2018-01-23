using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public Rigidbody2D sight;
    public Rigidbody2D character;
    
    public float maxDragDistance = 2f; //We can set it by the type of the selected weapon

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector3.Distance(mousePos, character.position) > maxDragDistance)
            sight.position = character.position + (mousePos - character.position).normalized * maxDragDistance;
        else
            sight.position = mousePos;
    }

    void OnMouseDown()
    {
        //Switch on weapon selected and FIREEEEEE if enough ammo

    }

    void OnMouseUp()
    {
        //For weapons that require letting the key press
    }
}
