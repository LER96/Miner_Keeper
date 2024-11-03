using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    [SerializeField] int slotsNumber;
    [SerializeField] Transform _upgradeSlotParent;
    [SerializeField] ItemUISlot _upgradeSlotItemPrefab;

    List<ItemUISlot> _itemsUI= new List<ItemUISlot>();
    List<ItemUISlot> _activeItems = new List<ItemUISlot>();
    ItemUISlot _item;

    public List<ItemUISlot> ActiveSlots => _activeItems;

    public void SetHandler()
    {
        InitSlots();
    }

    void InitSlots()
    {
        for (int i = 0; i < slotsNumber; i++)
        {
            _item = Instantiate(_upgradeSlotItemPrefab, _upgradeSlotParent);
            _itemsUI.Add(_item);
            _item.gameObject.SetActive(false);
        }
    }

    public void CreateItems(TowerCostSO towerCost)
    {
        ClearAll();
        for (int i = 0; i < towerCost.Cost.Count; i++)
        {
            _item = _itemsUI[i];
            _item.gameObject.SetActive(true);
            _item.SetData(towerCost.Cost[i].item);
            _item.UpdateResouceCount(towerCost.Cost[i].amount);
            _activeItems.Add(_item);
        }
    }

    public void UpgradeProgress(Item item)
    {
        for (int i = 0; i < _activeItems.Count; i++)
        {
            if (item.CompareItem(_activeItems[i].ItemName))
            {
                int amount = _activeItems[i].CurrentAmount - 1;
                if (amount <= 0)
                {
                    _activeItems[i].UpdateResouceCount(0);
                    _activeItems.Remove(_activeItems[i]);
                    if(_activeItems.Count==0)
                        UpgradeManager.Instance.TorretHandler.Upgrade();
                    return;
                }
                _activeItems[i].UpdateResouceCount(amount);
            }
        }
    }

    void ClearAll()
    {
        _activeItems.Clear();
        for (int i = 0; i < _itemsUI.Count; i++)
        {
            _itemsUI[i].gameObject.SetActive(false);
        }
    }

}
