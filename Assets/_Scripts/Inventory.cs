using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
[System.Serializable]
public class Inventory 
{
   [System.Serializable]
   public class Slot
   {
      public CollectableType CollectableType;
      public int Count;
      public int MaxAllowed;

      public Sprite iconSprite;
      public Slot()//setting up slots
      {
         CollectableType = CollectableType.None;
         Count = 0;
         MaxAllowed = 64;
      }

      public bool canAddItem()//count check
      {
         if (Count < MaxAllowed)
         {
            return true;
         }
         return true;
      }

      public void addItem(Collectibles_ item)//add + Increment count
      {
         this.CollectableType = item.collectableType;
         this.iconSprite = item.iconSprite;
         Count++;
      }

      public void RemoveItem()
      {
         if (Count > 0) //has atleast one item
         {
            //if(PlayerController.Instance.IsRemoved == true)
            Count--;
            if (Count == 0)//remove icon 
            {
               iconSprite = null;
               CollectableType = CollectableType.None;
            }
         }
      }
   }
   public List<Slot> slots = new List<Slot>();

   public Inventory(int num_Slots) 
   {
      for (int i = 0; i < num_Slots; i++)
      {
         Slot slot = new Slot();
         slots.Add(slot); // adding items to list
      }
   }

   public void AddItems(Collectibles_ item)
   {
      foreach (Slot slot in slots)//for already present in inventory 
      {
         if (slot.CollectableType == item.collectableType && slot.canAddItem())
         {
            slot.addItem(item);
            return;
         }
      }
      
      foreach (Slot slot in slots)//if not already present in inventory 
      {
         if (slot.CollectableType == CollectableType.None)
         {
            slot.addItem(item);
            return;
         }
      }
   }

   public void Remove(int index)
   {
      slots[index].RemoveItem();
   }
}
