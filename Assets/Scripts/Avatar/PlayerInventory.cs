using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerInventory : MonoBehaviour {

    public int current_item;
    private string sprite_background_item_selected;
    private string sprite_background_item;
    public Inventory inventory;
    public UserControl user_control;
    public void Start()
    {
        current_item = 0;
        sprite_background_item_selected = "Sprites/Inventory/buttonSquare_brown";
        sprite_background_item = "Sprites/Inventory/buttonSquare_brown_pressed";

        Item bow = ScriptableObject.CreateInstance<Item>(); //Ajout de l'arc de base
        bow.init("Sprites/bow_0", "Sprites/arrow", "Items/Bow/Bow_object", "Bow");
        inventory.AddItem(bow);
        user_control = GetComponent<UserControl>();
        SetPositionInventory(user_control.userNumber);
        
    }

    public bool DropObject()
    {
        string name_item = "";
        if (current_item != 0)
        {
            if (transform.Find("Canvas/Inventory").gameObject.GetComponent<Inventory>().IsItemSet(current_item))
            {
                
                name_item = inventory.NameOfItem(current_item);
                Instantiate(Resources.Load("Items/" + name_item + "/" + name_item + "_scene", typeof(GameObject)), transform.position, transform.rotation);
                inventory.RemoveItem(current_item);
                return true;
            }
        }
        return false;
    }

    private void change_background(int old_indice,int new_indice)
    {
        transform.Find("Canvas/Inventory/ItemSlot" + old_indice + "/BackgroundImage").gameObject.GetComponent<Image>().sprite = Resources.Load(sprite_background_item,typeof(Sprite)) as Sprite;
        transform.Find("Canvas/Inventory/ItemSlot" + new_indice + "/BackgroundImage").gameObject.GetComponent<Image>().sprite = Resources.Load(sprite_background_item_selected, typeof(Sprite)) as Sprite;

    }

    private void Update()
    {
        if(Input.GetButtonDown("Drop_Object") && current_item !=0)
        {
            DropObject();
        }

        if(Input.GetButtonDown("Item1"))
        {
            change_background(current_item, 0);
            current_item = 0;
            
        }
        if (Input.GetButtonDown("Item2"))
        {
            change_background(current_item, 1);
            current_item = 1;
        }
        if (Input.GetButtonDown("Item3"))
        {
            change_background(current_item, 2);
            current_item = 2;
        }
        if (Input.GetButtonDown("Item4"))
        {
            change_background(current_item, 3);
            current_item = 3;
        }
    }

    public Item GetCurrentItem()
    {
        return inventory.GetItem(current_item);
    }

    public void DestroyCurrentItem()
    {
        inventory.RemoveItem(current_item);
    }

    public void SetCurrentItem(int i)
    {
        current_item = i;
    }

    public void SetPositionInventory(int player_number)
    {
        switch(player_number)
        {
            case 0: transform.Find("Canvas/Inventory").position = new Vector3(1866 - 1000, 637+40, 0);
                break;
            case 1: transform.Find("Canvas/Inventory").position = new Vector3(1866 + 200, 637+40, 0);
                break;
            case 2:
                transform.Find("Canvas/Inventory").position = new Vector3(1866 - 1000, 637 + 80, 0); //TO DO
                break;
            case 3:
                transform.Find("Canvas/Inventory").position = new Vector3(1866 , 637 + 80, 0); //TO DO
                break;

        }
    }

}
