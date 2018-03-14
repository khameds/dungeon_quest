using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {


    [SerializeField]
    private PlayerInventory inventory;

    public Rigidbody2D character;
    private PlayerController playerController;
    Vector2 direction;
    float fireRate;
    float timeToFire;
    float startFire;
    public int userNumber;
    private bool ClickedButton;

    void Start()
    {
        
        inventory = GetComponent<PlayerInventory>();
        character = GetComponent<Rigidbody2D>();
        direction = GetComponent<SightingSystem>().direction;
        playerController = character.gameObject.GetComponent<PlayerController>();
        if (inventory == null || character == null || direction == null || playerController == null)
            Debug.LogError("[ShootingSystem] Fail to get all attribut references.");

        timeToFire = Time.time;
        ClickedButton = false;
    }



    // Update is called once per frame
    void Update () {
        
        Item item = inventory.GetCurrentItem();
        if (item != null)
        {
            GameObject current = item.itemObject;
            fireRate = current.GetComponent<Shoot>().fireRate;

            
            if(fireRate == 0)
            {
                if(userNumber==0) //Player 1 with keyboard+mouse
                {
                    if (Input.GetButtonDown("Fire"))
                    {
                        startFire = Time.time;
                    }
                    if (Input.GetButtonUp("Fire"))
                    {
                        if (current.GetComponent<Shoot>().shoot(item.name,userNumber, startFire - Time.time, character.transform.position, character, playerController.facingRight) == 0)
                        {
                            //Debug.Log("Remove Object");
                            inventory.DestroyCurrentItem();
                        }
                    }
                }
                else //Player 2/3/4 with gamepad
                {
                    if (GamepadManagement.getStateByUserNumber(userNumber).Triggers.Right != 0 && GamepadManagement.getPrevStateByUserNumber(userNumber).Triggers.Right == 0)
                    {
                        startFire = Time.time;
                    }
                    if (GamepadManagement.getStateByUserNumber(userNumber).Triggers.Right == 0 && GamepadManagement.getPrevStateByUserNumber(userNumber).Triggers.Right !=0)
                    {
                        if (current.GetComponent<Shoot>().shoot(item.name,userNumber, startFire - Time.time, character.transform.position, character, playerController.facingRight) == 0)
                        {
                            //Debug.Log("Remove Object");
                            inventory.DestroyCurrentItem();
                        }
                    }
                }
            }
            else
            {
                if(Time.time > timeToFire)
                {

                    if (userNumber == 0) //Player 1 with keyboard+mouse
                    {

                        if (Input.GetButtonDown("Fire"))
                        {
                            startFire = Time.time;
                            ClickedButton = true;
                        }
                        if (Input.GetButtonUp("Fire") && ClickedButton)
                        {
                            if (current.GetComponent<Shoot>().shoot(item.name,userNumber, startFire - Time.time, character.transform.position, character, playerController.facingRight) == 0)
                            {
                                //Debug.Log("Remove Object");
                                inventory.DestroyCurrentItem();
                                
                            }
                            timeToFire = Time.time + fireRate;
                            ClickedButton = false;
                        }
                    }
                    else //Player 2/3/4 with gamepad
                    {
                        if (GamepadManagement.getStateByUserNumber(userNumber).Triggers.Right != 0 && GamepadManagement.getPrevStateByUserNumber(userNumber).Triggers.Right == 0)
                        {
                            startFire = Time.time;
                        }
                        if (GamepadManagement.getStateByUserNumber(userNumber).Triggers.Right == 0 && GamepadManagement.getPrevStateByUserNumber(userNumber).Triggers.Right != 0)
                        {
                            if (current.GetComponent<Shoot>().shoot(item.name,userNumber, startFire - Time.time, character.transform.position, character, playerController.facingRight) == 0)
                            {
                                //Debug.Log("Remove Object");
                                inventory.DestroyCurrentItem();
                                
                            }
                            timeToFire += fireRate;
                        }
                    }

                    
                }
            }




        }
    }

    //Set the number of this player (0 => 3)
    internal void setNumber(int v)
    {
        userNumber = v;
    }
}
