using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDepositBrick : DepositBrick
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
        //            if (_torretHandler.CurrentTower.CanHeal())
        //            {
        //                _torretHandler.CurrentTower.HpChange(item.Value);
        //                _playerInventory.Deposit(item, this.transform);
        //                StartCoroutine(DepositCD());
        //            }
        //            return;
        //        }
        //    }
        //}
    }
}
