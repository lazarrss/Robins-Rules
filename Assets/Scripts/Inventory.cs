using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<Slot> listOfSlots = new List<Slot>();

    public Slot selectedSlot = null;

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            listOfSlots.Add(new Slot());
        }
    }

    public void Add(Item item)
    {
        foreach(Slot slot in listOfSlots)
        {
            if(slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach(Slot slot in listOfSlots)
        {
            if(slot.itemName == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        listOfSlots[index].RemoveItem();
    }

    public void Remove(int index, int quantity)
    {
        if (listOfSlots[index].count >= quantity)
        {
            for(int i = 0;i < quantity; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInv, int quantity = 1)
    {
        Slot fromSlot = listOfSlots[fromIndex];
        Slot toSlot = toInv.listOfSlots[toIndex];

        if(toSlot.IsEmpty() || toSlot.CanAddItem(fromSlot.itemName))
        {
            for(int i=0; i<quantity;i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed);
                fromSlot.RemoveItem();
            }
        }
    }

    public void SelectSlot(int index)
    {
        if(listOfSlots != null && listOfSlots.Count > 0)
        {
            selectedSlot = listOfSlots[index];
        }
    }

}
