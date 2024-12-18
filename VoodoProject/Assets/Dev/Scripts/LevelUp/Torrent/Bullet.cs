using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumHandler;

public class Bullet : MonoBehaviour
{
    public event Action OnHit;
    [SerializeField] Image _bulletImg;
    [SerializeField] BulletSO _mainBulletData;
    [SerializeField] BulletSO _specailBulletData;
    [SerializeField] float _timeAlive;

    BulletType _currentType;
    private BulletSO _currentBulletData;
    private float _damage;
    private float _explosion;
    private float _speed;
    private float _currentTimer;

    public float Damage { get => _damage; set => _damage = value; }
    public float Explosion { get => _explosion; set => _explosion = value; }
    public float Speed => _speed; //{ get => _speed; set => _speed = value; }

    //private void Start()
    //{
    //    _currentBulletData = _mainBulletData;
    //    SetData(_mainBulletData);
    //}

    private void Update()
    {
        Timer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetBulletType(BulletType bulletType)
    {
        _currentType = bulletType;
        switch(_currentType)
        {
            case BulletType.Regular:
                SetData(_mainBulletData);
                break;
            case BulletType.Special:
                SetData(_specailBulletData);
                break;
        }
    }

    void SetData(BulletSO data)
    {
        _currentBulletData = data;
        _bulletImg.sprite = _currentBulletData.BulletSprite;
        _bulletImg.color = _currentBulletData.BulletColor;
        _speed = _currentBulletData.Speed;

    }

    void Move()
    {
        transform.localPosition += transform.up * _speed;
    }

    void Timer()
    {
        if(_currentTimer<_timeAlive)
        {
            _currentTimer += Time.deltaTime;
        }
        else
        {
            ResetBullet();
        }    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            ResetBullet();
        }
        if (collision.transform.CompareTag("Enemy"))
        {
            SetDamage();
        }
    }

    void SetDamage()
    {
        Collider2D[] _enemies = Physics2D.OverlapCircleAll(transform.position, _explosion/2);
        for (int i = 0; i < _enemies.Length; i++)
        {
            Enemy enemy = _enemies[i].GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(_damage);
        }
        ResetBullet();
    }

    void ResetBullet()
    {
        _currentTimer = 0;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _explosion);
    }
}
