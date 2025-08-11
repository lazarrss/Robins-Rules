using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;
    private Dictionary<string, Item> nameItemDictionary = new Dictionary<string, Item>();

    private void Awake()
    {
        foreach(Item item in items)
        { 
            AddItem(item);
        }    
    }

    public void AddItem(Item item)
    {
        if(!nameItemDictionary.ContainsKey(item.data.itemName))
        {
            nameItemDictionary.Add(item.data.itemName, item);
        }
    }

    public Item GetItemByName(string key)
    {
        if(nameItemDictionary.ContainsKey(key))
            return nameItemDictionary[key];

        return null;
    }
}
