using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_scene : MonoBehaviour {

    private Item item = null;
    private bool picked_up = false;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Pick_Object"))
        {
            if (!picked_up)
                Pickup(other);
        }
    }


    private void Start()
    {
        Item shotgun = ScriptableObject.CreateInstance<Item>();
        shotgun.init("Sprites/shotgun_by_francotieppo-d5d99eg", "Sprites/Eye", "Items/Shotgun/Shotgun_object", "Shotgun");
        item = shotgun;
    }

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
            Debug.Log("[Shotgun_scene] Item not set");
        }
    }
}
