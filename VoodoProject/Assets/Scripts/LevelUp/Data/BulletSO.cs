using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "ScriptableObjects/Tower/Bullet")]
public class BulletSO : ScriptableObject
{
    [SerializeField] Sprite _bulletSprite;
    [SerializeField] float _damage;
    [SerializeField] float _speed;

    public Sprite BulletSprite => _bulletSprite;
    public float Damage => _damage;
    public float Speed => _speed;
}
