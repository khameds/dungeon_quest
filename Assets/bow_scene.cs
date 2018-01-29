﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bow_scene : MonoBehaviour {



    public Item item = null;
    private bool picked_up = false;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Pick_Object"))
        {
                if (!picked_up)
                    Pickup(other);
        }
    }
    // Update is called once per frame

    void Pickup(Collider2D player)
    {
        GameObject inventory = player.gameObject.transform.Find("Canvas/Inventory").gameObject;
        picked_up = true;

        if (item != null)
        {
            if (inventory.GetComponent<Inventory>().IsFull())
            {
                if (player.gameObject.GetComponent<PlayerInventory>().DropObject())
                {
                    inventory.GetComponent<Inventory>().AddItem(item);
                    Destroy(gameObject);
                }
                else
                {
                    picked_up = false;
                }

            }
            else
            {
                inventory.GetComponent<Inventory>().AddItem(item);
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("[Bow_scene] Item not set");
        }
    }
     
}
