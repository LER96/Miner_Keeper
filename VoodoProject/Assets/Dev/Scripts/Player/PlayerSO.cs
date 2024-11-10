using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] int level;
    [SerializeField] float Hp;
    [SerializeField] float _speed;
    [SerializeField] float _miningRate;
    [SerializeField] float _miningDamage;

    public int Level => level;
    public float HP => Hp;
    public float MovementSpeed => _speed;
    public float MiningRate => _miningRate;
    public float MiningDamage => _miningDamage;
}
