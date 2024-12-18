using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] Image _itemImg;

    private ItemSO _itemData;
    private TorretHandler _towerHandler;
    private TowerUpgradeHandler _ugprade;

    private void OnEnable()
    {
        _towerHandler = UpgradeManager.Instance.TorretHandler;
        _ugprade = UpgradeManager.Instance.TowerUpgradeHandler;
    }

    public void SetData(ItemSO itemData)
    {
        _itemData = itemData;
        _itemImg.sprite = itemData.ItemSprite;
    }

    public void SetDestination(Vector3 target, float duration)
    {
        switch (_itemData.ItemName)
        {
            case "Sapphire":
                _ugprade.UpgradeProgress(_itemData, _itemData.Value);
                break;
            case "Coins":
                break;
            case "Quartz":
                _towerHandler.CurrentTower.ReloadSpecialAmmo(_itemData.Value);
                break;
            case "Nitre":
                _towerHandler.CurrentTower.HpChange(_itemData.Value);
                break;
            default:
                break;
        }
        transform.DOMove(target, duration).OnComplete(ResetDrop);
    }

    void ResetDrop()
    {
        transform.localPosition = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}
