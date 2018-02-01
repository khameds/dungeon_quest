﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SightingSystem : MonoBehaviour
{
    
    public GameObject sight;
    public Rigidbody2D character;
    private Vector2 direction, mousePos;
    private SpriteRenderer spriteRenderer;
    private float sensibility = 2f;
    private float angle;
    private PlayerController playerController;

    [SerializeField] private PlayerInventory inventory;
    // Property for weapon use
    public Vector2 Direction
    {
        get { return direction; }
    }



    public float maxDragDistance = .2f; //We can set it by the type of the selected weapon
    void Awake()
    {
        character = GetComponent<Rigidbody2D>();
        playerController = character.gameObject.GetComponent<PlayerController>();
        inventory = GetComponent<PlayerInventory>();
        spriteRenderer = sight.GetComponent<SpriteRenderer>();

        if (character == null || inventory == null || spriteRenderer == null)
            Debug.LogError("[SightingSystem] Fail to get all attribut references.");
    }
    
    void Start()
    {
        //Set Cursor to not be visible
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
   
        // Flip
        //sight.transform.localScale = transform.localScale * -1;
    
    }

    void Update()
    {
        Item item = inventory.GetCurrentItem();
        if (item != null)
            spriteRenderer.sprite = item.descriptionSprite;
        else
            spriteRenderer.sprite = null;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - character.position).normalized;

        //direction.x = Mathf.Abs(direction.x);
        print("Shot direction : " + direction);


        angle = GetAngle(character.position, mousePos);
        angle = (playerController.facingRight) ? - angle : angle;
        sight.transform.rotation = Quaternion.Euler(0f, 0f, angle); ;
    }

    float GetAngle(Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v1.y - v2.y,Mathf.Abs( v1.x - v2.x) ) * Mathf.Rad2Deg;
    }



}
