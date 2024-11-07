using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action OnHit;
    [SerializeField] BulletSO _mainBulletData;
    [SerializeField] BulletSO _specailBulletData;
    [SerializeField] float _timeAlive;

    private BulletSO _currentBulletData;
    private float _damage;
    private float _speed;
    private float _currentTimer;

    public float Damage { get => _damage; set => _damage = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private void Start()
    {
        _currentBulletData = _mainBulletData;
        SetData();
    }

    private void Update()
    {
        Timer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void SetData()
    {
        _damage = _mainBulletData.Damage;
        _speed = _mainBulletData.Speed;
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
        //else if(collision.transform.CompareTag("Enemy"))
        //{
        //    
        //}
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
}
