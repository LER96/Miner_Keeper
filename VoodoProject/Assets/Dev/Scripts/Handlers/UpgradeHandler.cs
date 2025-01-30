using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] TMP_Text _xpText;
    [SerializeField] float _duration;

    [Header("Upgrades")]
    [SerializeField] GameObject _cardHolder;
    [SerializeField] List<UpgradeCard> _cards = new List<UpgradeCard>();
    private List<UpgradeSO> _collectUpgrades = new List<UpgradeSO>();

    int index=0;
    private int _currentLevel;
    private XpSO _currentXPData;
    private float _currentXP;
    private float _maxXP;
    private float _xpLeft;
    private Transform _currentDropItem;
    private TorretHandler _towerHandler;

    private Queue<ItemDrop> drops = new Queue<ItemDrop>();

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
        _collectUpgrades.Clear();
        _currentLevel = index;
        if(index>_xpLevel.Count)
        {
            return;
        }
        _currentXPData = _xpLevel[index-1];
        _maxXP = _currentXPData.XPCapacity;
        for (int i = 0; i < _currentXPData.Upgrades.Count; i++)
        {
            _collectUpgrades.Add(_currentXPData.Upgrades[i]);
        }
       //_currentXP += _xpLeft;
        UpdateXPBar();
    }

    public void DropItem(ItemSO item, Transform from)
    {
        if (item == null)
            return;

        ItemDrop _drop = drops.Dequeue();
        _drop.gameObject.SetActive(true);
        Vector3 pos = Camera.main.WorldToScreenPoint(from.position);
        _drop.transform.position = pos;
        _drop.SetData(item);
        SetDestination(item, _drop);
        drops.Enqueue(_drop);
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
            Upgrade();
            SetData(_currentLevel + 1);
            _currentXP = _tempXP;
        }
        UpdateXPBar();
    }

    void UpdateXPBar()
    {
        _xpText.text = $"{_maxXP - _currentXP}";
        _xpSlider.value = _currentXP / _maxXP;
    }

    void Upgrade()
    {
        Time.timeScale = 0;
        SetCards(true);
        for (int i = 0; i < _cards.Count; i++)
        {

            int rnd = Random.Range(0, _collectUpgrades.Count - 1);
            UpgradeSO upgrade = _collectUpgrades[rnd];
            _cards[i].SetData(upgrade);
            _collectUpgrades.Remove(upgrade);

        }
    }

    void SetCards(bool active)
    {
        _cardHolder.SetActive(active);
    }

    public void DisableCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetToOrigin();
        }
        Time.timeScale = 1;
        SetCards(false);
    }

}
