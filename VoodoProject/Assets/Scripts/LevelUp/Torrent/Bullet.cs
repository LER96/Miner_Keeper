using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action OnHit;
    [SerializeField] float _damage;
    [SerializeField] float _speed;
    [SerializeField] float _timeAlive;

    float _currentTimer;

    public float Damage { get => _damage; set => _damage = value; }

    private void Update()
    {
        Timer();
    }

    private void FixedUpdate()
    {
        Move();
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
        //    OnHit.Invoke();
        //}
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
}
