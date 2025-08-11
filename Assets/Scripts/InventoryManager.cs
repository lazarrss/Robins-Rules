using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> invetoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    private void Awake()
    {
        backpack = new Inventory(backpackSlotCount);
        toolbar = new Inventory(toolbarSlotCount);

        invetoryByName.Add("Backpack", backpack);
        invetoryByName.Add("Toolbar", toolbar);
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if(invetoryByName.ContainsKey(inventoryName)) return invetoryByName[inventoryName];
        return null;
    }

    public void Add(string inventoryName, Item item)
    {
        if(invetoryByName.ContainsKey(inventoryName))
        {
            invetoryByName[inventoryName].Add(item);
        }
    }

}
