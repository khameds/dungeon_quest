using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bow_scene : MonoBehaviour {



    public Item item = null;
    private GameObject Inventory;
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

        picked_up = true;    
        
        if (item != null) { 
            if(player.gameObject.transform.Find("Canvas/Inventory").gameObject.GetComponent<Inventory>().IsFull())
                player.gameObject.GetComponent<PlayerInventory>().DropObject();

            player.gameObject.transform.Find("Canvas/Inventory").gameObject.GetComponent<Inventory>().AddItem(item);
        }
        else
        {
            Debug.Log("Item not set");
        }
        
        
        Destroy(gameObject);
    }
}
