using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSO : ScriptableObject
{
    [SerializeField] protected Sprite _upgardeSprite;
    [SerializeField] protected string _upgradeName;
    [SerializeField] protected string _description;
    [SerializeField] protected float _value;

    protected Tower _tower;
    public Sprite UpgradeImg => _upgardeSprite;
    public string UpgradeName => _upgradeName;
    public string Description => _description;

    public virtual void SetUpgrade()
    {
        _tower = UpgradeManager.Instance.TorretHandler.CurrentTower;
    }
}
