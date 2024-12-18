using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositAmmoBrick : DepositBrick
{
    [SerializeField] ItemSO _ammoItem;
    private TorretHandler _torretHandler;

    protected override void Start()
    {
        base.Start();
        _torretHandler = UpgradeManager.Instance.TorretHandler;
    }

    protected override void CheckResources()
    {
        //if (_playerInventory.AllItems.Count > 0)
        //{
        //    foreach (Item item in _playerInventory.AllItems)
        //    { 
        //        if (item.CompareItem(_ammoItem.ItemName))
        //        {
        //            if (_torretHandler.CurrentTower.CanRelaod())
        //            {
        //                _torretHandler.CurrentTower.ReloadSpecialAmmo(1);
        //                _playerInventory.Deposit(item, this.transform);
        //                StartCoroutine(DepositCD());
        //            }
        //            return;
        //        }
        //    }
        //}
    }
}
