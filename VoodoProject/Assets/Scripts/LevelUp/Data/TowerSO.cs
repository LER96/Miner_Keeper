using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "ScriptableObjects/Tower/Level")]
public class TowerSO : ScriptableObject
{
    [SerializeField] int _level;
    [SerializeField] Sprite _towerSprite;
    [SerializeField] Sprite _gunSprite;
    [SerializeField] float _attackRate;
    [SerializeField] float _damage;

    public int TowerLevel => _level;
    public float AttackRate=> _attackRate;
    public float Damage => _damage;
    public Sprite TowerSprit => _towerSprite;
    public Sprite GunSprite => _towerSprite;
}
