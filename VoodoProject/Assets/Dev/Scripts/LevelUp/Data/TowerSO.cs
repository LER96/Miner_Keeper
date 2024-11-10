using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "ScriptableObjects/Tower/Level")]
public class TowerSO : ScriptableObject
{
    [SerializeField] int _level;
    [SerializeField] Sprite _towerSprite;
    [SerializeField] Sprite _gunSprite;

    [SerializeField] float _damage;
    [SerializeField] float _attackRate;
    [SerializeField] float _rotationSpeed;
    [SerializeField] int _maxCapacity;

    //[SerializeField] ItemSO itemAmmo;
    [SerializeField] TowerCostSO _towerCostData;

    public int TowerLevel => _level;
    public Sprite TowerSprit => _towerSprite;
    public Sprite GunSprite => _towerSprite;
    public float Damage => _damage;
    public float AttackRate=> _attackRate;
    public float Agility => _rotationSpeed;
    public int MaxCapacity => _maxCapacity;
    public TowerCostSO TowerCost => _towerCostData;

}
