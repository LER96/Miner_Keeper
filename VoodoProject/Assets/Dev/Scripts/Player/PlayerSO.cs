using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] int level;
    [SerializeField] float _speed;
    [SerializeField] float _miningRate;
    [SerializeField] float _miningDamage;

    public float MovementSpeed => _speed;
    public float MiningRate => _miningRate;
    public float MiningDamage => _miningDamage;
}
