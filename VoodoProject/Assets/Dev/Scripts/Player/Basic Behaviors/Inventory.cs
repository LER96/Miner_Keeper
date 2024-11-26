using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] int _itemNumber;
    [SerializeField] Transform _itemParent;
    [SerializeField] Item itemPrefab;
    Transform _target;

    [Header("Slot")]
    [SerializeField] Transform _slotParent;
    [SerializeField] ItemUISlot slotItemPrefab;

    Item _itemToRemove;
    Item _item;
    Queue<Item> _actualItems = new Queue<Item>();
    List<Item> _allItems = new List<Item>();
    Dictionary<ItemUISlot, int> _inventory = new Dictionary<ItemUISlot, int>();

    public List<Item> AllItems => _allItems;

    void Start()
    {
        InitItemObject();
    }

    void InitItemObject()
    {
        for (int i = 0; i < _itemNumber; i++)
        {
            Item item = Instantiate(itemPrefab, _itemParent);
            _actualItems.Enqueue(item);
        }
    }

    void UpdateAmount(string itemName, int count)
    {
        foreach (var slotItem in _inventory.ToArray())
        {
            string slotName = slotItem.Key.ItemName;
            if (itemName == slotName)
            {
                
                _inventory[slotItem.Key] += count;
                ItemUISlot itemUISlot = slotItem.Key;
                itemUISlot.transform.SetAsFirstSibling();
                itemUISlot.UpdateResouceCount(_inventory[slotItem.Key]);

                if (_inventory[slotItem.Key] < 0)
                    _inventory[slotItem.Key] = 0;
            }
        }
    }

    void OnDeposit()
    {
        RemoveItem(_itemToRemove);
        _actualItems.Enqueue(_item);
        _item.gameObject.SetActive(false);
    }

    void RemoveItem(Item item)
    {
        if(_allItems.Contains(item)==false)
        {
            return;
        }

        foreach (Item currentItem in _allItems)
        {
            if (item.CompareItem(currentItem))
            {
                if (currentItem.Value > 0)
                {
                    currentItem.Value -= 1;
                    UpdateAmount(item.ItemName, -1);
                }
                else
                {
                    _allItems.Remove(currentItem);
                    return;
                }
            }
        }
    }

    public void Deposit(Item itemRemove, Transform target)
    {
        _target = target;
        _itemToRemove = itemRemove;
        _item = _actualItems.Dequeue();
        _item.gameObject.SetActive(true);
        _item.SetData(itemRemove.ItemName, itemRemove.ItemSprite);
        _item.transform.DOJump(_target.position, 2, 1, 0.1f).OnComplete(OnDeposit);
    }

    public void AddItem(Item item)
    {
        foreach (ItemUISlot currentItem in _inventory.Keys.ToArray())
        {
            if(item.CompareItem(currentItem.ItemName))
            {
                _allItems.Add(item);
                UpdateAmount(item.ItemName, item.Value);
                return;
            }
        }

        _allItems.Add(item);
        ItemUISlot itemUISlot = Instantiate(slotItemPrefab, _slotParent);
        itemUISlot.SetData(item);
        itemUISlot.UpdateResouceCount(item.Value);
        _inventory.Add(itemUISlot, item.Value);
    }


}
