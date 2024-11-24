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

    [Header("Xp Bar")]
    [SerializeField] List<XpSO> _xpLevel = new List<XpSO>();
    [SerializeField] Transform _xpPoint;
    [SerializeField] Slider _xpSlider;
    [SerializeField] float _duration;
    private XpSO _currentXPData;
    private float _currentXP;
    private float _maxXP;
    private float _xpLeft;
    private Transform _currentDropItem;

    Transform _resourceSpot;
    Queue<ItemDrop> drops = new Queue<ItemDrop>();

    public void SetHandler()
    {
        InitDrops();
        SetData(0);
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
        if(index>_xpLevel.Count)
        {
            return;
        }
        _currentXPData = _xpLevel[index];
        _maxXP = _currentXPData.XPCapacity;
        _currentXP += _xpLeft;
    }

    public void DropItem(Enemy enemy)
    {
        ItemSO enemyDrop = enemy.EnemyData.DropItem;
        ItemDrop _drop = drops.Dequeue();
        _drop.gameObject.SetActive(true);
        _drop.transform.position = enemy.transform.position;
        _drop.SetData(enemyDrop);
        _currentDropItem = _drop.transform;
        SetDestination(enemyDrop);
        drops.Enqueue(_drop);
    }

    void SetDestination(ItemSO item)
    {
        switch(item.ItemName)
        {
            case "XP":
                _currentXP += item.Value;
                _currentDropItem.DOJump(_xpPoint.position, 10, 1, _duration).OnComplete(ResetCurrentDrop);
                break;
            default:
                break;
        }
    }

    void ResetCurrentDrop()
    {
        _currentDropItem.localPosition = Vector3.zero;
        _currentDropItem.gameObject.SetActive(false);
        _xpSlider.value = _currentXP / _maxXP;
    }
}
