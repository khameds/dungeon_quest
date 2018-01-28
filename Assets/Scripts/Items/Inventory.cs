using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public const int maxItemSlots = 4;
    public Sprite[] itemImages = new Sprite[maxItemSlots];
    public Item[] items = new Item[maxItemSlots];
    private void Awake()
    {
        
    }
    
    public bool IsFull()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                return false;
        }
        return true;
    }
    public void AddItem(Item itemToAdd)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i] = itemToAdd.descriptionSprite;
                transform.Find("ItemSlot" + i + "/ItemImage").gameObject.GetComponent<Image>().sprite = itemImages[i];
                transform.Find("ItemSlot" + i + "/ItemImage").gameObject.GetComponent<Image>().enabled = true;

                //transform.Find("ItemSlot" + i + "/BackgroundImage").gameObject.GetComponent<Image>().sprite = itemImages[i];

                return;
            }
        }
    }

    public void RemoveItem(int itemToRemove)
    {
       items[itemToRemove] = null;
       itemImages[itemToRemove]= null;
       transform.Find("ItemSlot" + itemToRemove + "/ItemImage").gameObject.GetComponent<Image>().sprite = null;
       transform.Find("ItemSlot" + itemToRemove + "/ItemImage").gameObject.GetComponent<Image>().enabled = false;
    }

    public bool IsItemSet(int indice)
    {
        if (items[indice] == null)
            return false;
        return true;
    }

    public string NameOfItem(int indice)
    {
        return items[indice].ToString().Split(' ')[0];
    }

    public Item GetItem(int indice)
    {
        return(items[Mathf.Clamp(indice, 0, maxItemSlots - 1)]);
    }

}
