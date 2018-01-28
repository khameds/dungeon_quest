using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SightingSystem : MonoBehaviour
{
    
    public GameObject sight;
    private Rigidbody2D rbSight;
    public Rigidbody2D character;
    private Vector2 direction;
    private SpriteRenderer spriteRenderer;


    private Vector2 oldMousePosition;
    private Vector2 oldDirection;
    private float sensibility = 2f;

    [SerializeField] private PlayerInventory inventory;
    // Property for weapon use
    public Vector2 Direction
    {
        get { return direction; }
    }



    public float maxDragDistance = 2f; //We can set it by the type of the selected weapon
    void Awake()
    {
        rbSight = sight.GetComponent<Rigidbody2D>();
        character = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory>();
        spriteRenderer = sight.GetComponent<SpriteRenderer>();

        if (rbSight == null || character == null || inventory == null || spriteRenderer == null)
            Debug.LogError("[SightingSystem] Fail to get all attribut references.");
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
        Item item = inventory.GetCurrentItem();
        if (item != null)
            spriteRenderer.sprite = item.ammoSprite;
        else
            spriteRenderer.sprite = null;



        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - character.position).normalized;
        direction = (oldDirection + (mousePos - oldMousePosition).normalized).normalized;
        if (Vector3.Distance(mousePos, character.position) > maxDragDistance)
            rbSight.position = character.position + direction * maxDragDistance;
         else
           rbSight.position = mousePos;
/*
        //https://docs.unity3d.com/ScriptReference/Vector2.SmoothDamp.html
        if (Vector2.Scale(mousePos - oldMousePosition) > sensibility)
        {
            oldMousePosition = mousePos;
            oldDirection = (mousePos - character.position).normalized;
        }

    */
    }


    
}
