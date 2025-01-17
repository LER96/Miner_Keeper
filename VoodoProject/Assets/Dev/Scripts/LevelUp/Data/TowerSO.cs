using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "ScriptableObjects/Tower/Level")]
public class TowerSO : ScriptableObject
{
    [SerializeField] int _level;
    [SerializeField] Sprite _towerSprite;
    [SerializeField] Sprite _gunSprite;

    [SerializeField] float _basicDamage;
    [SerializeField] float _enhancedDamage;
    [SerializeField] float _omniPower;
    [SerializeField] float _expolsionRadius;
    [SerializeField] float _critChance;
    [SerializeField] float _critMultiplier;
    [SerializeField] float _attackRate;
    [SerializeField] float _rotationSpeed;
    [SerializeField] int _maxCapacity;

    //[SerializeField] ItemSO itemAmmo;
    [SerializeField] TowerCostSO _towerCostData;

    public int TowerLevel => _level;
    public Sprite TowerSprit => _towerSprite;
    public Sprite GunSprite => _gunSprite;
    public float Damage => _basicDamage;
    public float Omni => _omniPower;
    public float ExplosionRadius => _expolsionRadius;
    public float CritChance => _critChance;
    public float CritMultiplier => _critMultiplier;
    public float SpecialDamage => _enhancedDamage;
    public float AttackRate=> _attackRate;
    public float Agility => _rotationSpeed;
    public int MaxCapacity => _maxCapacity;
    public TowerCostSO TowerCost => _towerCostData;

}
