using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] private List<SlotsUI> toolbarSlots = new List<SlotsUI>();

    private SlotsUI selectedSlot;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {
        CheckAlphaNumKeys();
    }

    public void SelectSlot(int idx)
    {
        if(toolbarSlots.Count > 0) 
        {
            if(selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[idx];
            selectedSlot.SetHighlight(true);
            Debug.Log("Selected slot: " + selectedSlot.name);

            GameManager.instance.player.inventory.toolbar.SelectSlot(idx);
        }
    }

    private void CheckAlphaNumKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSlot(0);
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            SelectSlot(1);
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            SelectSlot(2);
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            SelectSlot(3);
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            SelectSlot(4);
        else if(Input.GetKeyDown(KeyCode.Alpha6))
            SelectSlot(5);
        else if(Input.GetKeyDown(KeyCode.Alpha7))
            SelectSlot(6);
        else if(Input.GetKeyDown(KeyCode.Alpha8))
            SelectSlot(7);
        else if(Input.GetKeyDown(KeyCode.Alpha9))
            SelectSlot(8);
    }
}
