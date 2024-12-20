using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [Header("Drop Pool")]
    [SerializeField] int _numerOfDrops;
    [SerializeField] Transform _dropParent;
    [SerializeField] ItemDrop _dropItemPrefab;

    [Header("Xp")]
    [SerializeField] List<XpSO> _xpLevel = new List<XpSO>();
    [SerializeField] Transform _xpPoint;
    [SerializeField] Slider _xpSlider;
    [SerializeField] float _duration;

    private int _currentLevel;
    private XpSO _currentXPData;
    private float _currentXP;
    private float _maxXP;
    private float _xpLeft;
    private Transform _currentDropItem;
    private TorretHandler _towerHandler;

    Queue<ItemDrop> drops = new Queue<ItemDrop>();

    public void SetHandler()
    {
        InitDrops();
        SetData(1);
    }

    void InitDrops()
    {
        for (int i = 0; i < _numerOfDrops; i++)
        {
            ItemDrop item = Instantiate(_dropItemPrefab, _dropParent);
            drops.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }

    void SetData(int index)
    {
        _currentLevel = index;
        if(index>_xpLevel.Count)
        {
            return;
        }
        _currentXPData = _xpLevel[index-1];
        _maxXP = _currentXPData.XPCapacity;
        _currentXP += _xpLeft;
    }

    public void DropItem(ItemSO item, Transform from)
    {
        if (item != null)
        {
            ItemDrop _drop = drops.Dequeue();
            _drop.gameObject.SetActive(true);
            Vector3 pos = Camera.main.WorldToScreenPoint(from.position);
            _drop.transform.position = pos;
            _drop.SetData(item);
            SetDestination(item, _drop);
            drops.Enqueue(_drop);
        }
    }

    void SetDestination(ItemSO item, ItemDrop drop)
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerManger.Instance.PlayerHandler.transform.position);
        switch (item.ItemName)
        {
            case "Sapphire":
                _currentXP += item.Value;
                UpdateXP();
                drop.SetDestination(_xpPoint.position, _duration);
                break;
            default:
                drop.SetDestination(playerPos, _duration);
                break;
        }
    }

    private void UpdateXP()
    {
        if (_currentXP >= _maxXP)
        {
            float _tempXP = _currentXP - _maxXP;
            SetData(_currentLevel+1);
            UpgradeManager.Instance.TorretHandler.Upgrade();
            _currentXP = _tempXP;
        }
        _xpSlider.value = _currentXP / _maxXP;
    }

}
