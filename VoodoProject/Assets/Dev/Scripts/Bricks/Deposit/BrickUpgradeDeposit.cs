using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickUpgradeDeposit : DepositBrick
{

    protected override void CheckResources()
    {
        if (_playerInventory.AllItems.Count > 0)
        {
            foreach (Item item in _playerInventory.AllItems)
            {
                for (int i = 0; i < _upgradeHandler.ActiveSlots.Count; i++)
                {
                    ItemUISlot itemUI = _upgradeHandler.ActiveSlots[i];
                    if (item.CompareItem(itemUI.ItemName))
                    {
                        _playerInventory.Deposit(item,this.transform);
                        _upgradeHandler.UpgradeProgress(item);
                        StartCoroutine(DepositCD());
                        return;
                    }
                }
            }
        }
    }

}
