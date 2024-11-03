using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDeposit : MonoBehaviour
{
    [SerializeField] float _itemDepositDelay;
    [SerializeField] bool canCollect;
    private Transform _target;
    private UpgradeHandler _upgradeHandler;
    private Inventory _playerInventory;


    private void Start()
    {
        _playerInventory = PlayerManger.Instance.PlayerInventory;
        _upgradeHandler = UpgradeManager.Instance.UpgradeHandler;
        canCollect = true;
    }

    private void Update()
    {
        if(_target!=null && canCollect)
        {
            canCollect = false;
            CheckResources();
        }
    }

    void CheckResources()
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
                        _upgradeHandler.UpgradeProgress(item);
                        _playerInventory.Deposit(item,this.transform);
                        StartCoroutine(DepositCD());
                        return;
                    }
                }
            }
        }

    }

    IEnumerator DepositCD()
    {
        yield return new WaitForSeconds(_itemDepositDelay);
        canCollect = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && canCollect)
        {
            _target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _target = null;
            canCollect = true;
        }
    }

}
