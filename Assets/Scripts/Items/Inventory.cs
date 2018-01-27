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

    public void AddItem(Item itemToAdd)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i] = itemToAdd.sprite;
                transform.Find("ItemSlot" + i + "/ItemImage").gameObject.GetComponent<Image>().sprite = itemImages[i];
                transform.Find("ItemSlot" + i + "/ItemImage").gameObject.GetComponent<Image>().enabled = true;

                //transform.Find("ItemSlot" + i + "/BackgroundImage").gameObject.GetComponent<Image>().sprite = itemImages[i];

                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                itemImages[i]= null;
                return;
            }
        }
    }

}
