using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public List<Slots_UI> slots = new List<Slots_UI>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            SetUp();
        }
        else { inventoryPanel.SetActive(false);}
    }

    void SetUp()
    {
        if (slots.Count == PlayerController.Instance.Inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (PlayerController.Instance.Inventory.slots[i].CollectableType != CollectableType.None)//if not none then there is item in the inventory
                {
                    slots[i].SetItem(PlayerController.Instance.Inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int SlotIndex)
    {
        Collectibles_ itemToDrop =
            GameManager.Instance.ItemManager.GetItemByType((PlayerController.Instance.Inventory.slots[SlotIndex]
                .CollectableType));
        if (itemToDrop != null)//check if we can drop
        {
            PlayerController.Instance.DropItem(itemToDrop);
            PlayerController.Instance.Inventory.Remove(SlotIndex);
            SetUp();
        }
    }
}
