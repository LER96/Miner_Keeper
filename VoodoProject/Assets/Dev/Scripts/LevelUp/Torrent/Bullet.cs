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
    private float _speed;
    private float _currentTimer;

    public float Damage => _damage;// { get => _damage; set => _damage = value; }
    public float Speed => _speed; //{ get => _speed; set => _speed = value; }

    private void Start()
    {
        _currentBulletData = _mainBulletData;
        SetData(_mainBulletData);
    }

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
        _damage = _currentBulletData.Damage;
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
            _currentTimer = 0;
            gameObject.SetActive(false);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Floor"))
            this.gameObject.SetActive(false);
        else if (collision.transform.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(_damage);
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
}
