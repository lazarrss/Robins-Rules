using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public string inventoryName;

    public List<SlotsUI> listOfSlots = new List<SlotsUI>();

    [SerializeField] private Canvas canvas;

    private bool dragSingle = false;

    private Inventory inventory;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);

        SetupSlots();
        Refresh();
    }

    public void Refresh()
    {
        if(listOfSlots.Count == inventory.listOfSlots.Count)
        {
            for (int i = 0; i < listOfSlots.Count; i++)
            {
                if (inventory.listOfSlots[i].itemName != "")
                {
                    listOfSlots[i].SetItem(inventory.listOfSlots[i]);
                }
                else
                {
                    listOfSlots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.listOfSlots[UIManager.draggedSlot.slotNum].itemName);

        if (itemToDrop != null)
        {
            if(UIManager.dragSingle == false)
            {
                GameManager.instance.player.DropItem(itemToDrop, inventory.listOfSlots[UIManager.draggedSlot.slotNum].count);
                inventory.Remove(UIManager.draggedSlot.slotNum, inventory.listOfSlots[UIManager.draggedSlot.slotNum].count);
            }
            else
            {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(UIManager.draggedSlot.slotNum);
            }
            Refresh();
        }

        UIManager.draggedSlot = null;
    }

    public void SlotBeginDrag(SlotsUI slot)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
        UIManager.draggedSlot = slot;
        UIManager.draggedIcon = Instantiate(UIManager.draggedSlot.itemIcon);
        UIManager.draggedIcon.transform.SetParent(canvas.transform);
        UIManager.draggedIcon.raycastTarget = false;
        UIManager.draggedIcon.rectTransform.sizeDelta = new Vector2(75,75);

        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        Debug.Log("Begin drag: " + UIManager.draggedSlot.name);
    }
    public void SlotEndDrag()
    {
        Destroy(UIManager.draggedIcon.gameObject);
        //draggedIcon = null;
        Debug.Log("End drag: " + UIManager.draggedSlot.name);
    }
    public void SlotDrag()
    {
        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        Debug.Log("Dragging: " + UIManager.draggedSlot.name);
    }
    public void SlotDrop(SlotsUI slot)
    {
        Debug.Log("Dropped: " + slot.slotNum);

        if(UIManager.dragSingle)
        {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotNum, slot.slotNum, slot.inventory);
        }
        else
        {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotNum, slot.slotNum, slot.inventory, UIManager.draggedSlot.inventory.listOfSlots[UIManager.draggedSlot.slotNum].count);
        }

        GameManager.instance.uiManager.RefreshAll();
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    private void SetupSlots()
    {
        int counter = 0;

        foreach(SlotsUI slot in listOfSlots)
        {
            slot.slotNum = counter++;
            slot.inventory = inventory;
        }
    }
}
