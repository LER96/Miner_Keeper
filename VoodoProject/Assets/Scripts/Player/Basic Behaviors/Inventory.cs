using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform _slotParent;
    [SerializeField] ItemUISlot slotItemPrefab;
    List<Item> _allItems = new List<Item>();
    Dictionary<ItemUISlot, int> _inventory = new Dictionary<ItemUISlot, int>();

    public void AddItem(Item item)
    {
        foreach (Item currentItem in _allItems)
        {
            if(item.ItemName== currentItem.ItemName)
            {
                _allItems.Add(item);
                UpdateAmount(item, item.Value);
                return;
            }
        }

        ItemUISlot itemUISlot = Instantiate(slotItemPrefab, _slotParent);
        itemUISlot.SetData(item);
        itemUISlot.UpdateResouceCount(item.Value);
        _inventory.Add(itemUISlot, item.Value);
    }

    public void RemoveItem(Item item)
    {
        foreach (Item currentItem in _allItems)
        {
            if (item.ItemName == currentItem.ItemName)
            {
                UpdateAmount(item, -1);
                return;
            }
        }
    }

    private void UpdateAmount(Item item, int count)
    {
        foreach(var slotItem in _inventory.Keys)
        {
            Item slot = slotItem.CurrentItem;
            if (item.ItemName == slot.ItemName)
            {
                _inventory[slotItem] += count;
                if (_inventory[slotItem] < 0)
                    _inventory[slotItem] = 0;
            }
        }
    }
}
