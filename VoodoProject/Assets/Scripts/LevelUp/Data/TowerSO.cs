using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSO : ScriptableObject
{
    [SerializeField] float _attackRate;
    [SerializeField] float _damage;

    public float AttackRate=> _attackRate;
    public float Damage => _damage;
}
