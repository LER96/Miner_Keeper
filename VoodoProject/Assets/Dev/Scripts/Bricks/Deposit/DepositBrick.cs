using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBrick : MonoBehaviour
{
    [SerializeField] protected float _itemDepositDelay;
    protected bool canCollect;
    protected Transform _target;
    protected TowerUpgradeHandler _upgradeHandler;
    protected Inventory _playerInventory;

    protected virtual void Start()
    {
        _playerInventory = PlayerManger.Instance.PlayerInventory;
        _upgradeHandler = UpgradeManager.Instance.UpgradeHandler;
        canCollect = true;
    }
    protected virtual void Update()
    {
        if (_target != null && canCollect)
        {
            canCollect = false;
            CheckResources();
        }
    }

    protected virtual void CheckResources()
    {
        
    }

    protected virtual IEnumerator DepositCD()
    {
        yield return new WaitForSeconds(_itemDepositDelay);
        canCollect = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && canCollect)
        {
            _target = collision.transform;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _target = null;
            canCollect = true;
        }
    }
}
