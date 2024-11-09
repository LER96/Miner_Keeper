using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] string _enemyName;
    [SerializeField] float _hp;
    [SerializeField] float _speed;

    [SerializeField] float _damage;
    [SerializeField] float _attackRange;
    [SerializeField] float _attackRate;

    public string EnemyName => _enemyName;
    public float HP => _hp;
    public float MovementSpeed => _speed;
    public float Damage => _damage;
    public float AttackRange => _attackRange;
    public float AttackRate => _attackRate;
}
