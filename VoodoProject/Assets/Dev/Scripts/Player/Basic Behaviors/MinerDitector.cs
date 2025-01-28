using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerDitector : MonoBehaviour
{
    [SerializeField] Brick _target;
    [SerializeField] MMFeedbacks feedBack;
    [SerializeField] ParticleSystem _hitVFX;
    [SerializeField] LayerMask _layer;

    private Collider2D _collide;
    private bool _checkForTarget;
    private PlayerMining _playerMining;

    private void Start()
    {
        _playerMining = PlayerManger.Instance.MiningScript;
        _playerMining.OnHit += ApplyDamage;
    }

    private void Update()
    {
        if (_checkForTarget)
            CheckForTarget(_collide);
    }

    protected virtual void CheckForTarget(Collider2D collide)
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), transform.localScale.x / 2, _layer);
        _target = collide.GetComponent<Brick>();
        if (_target!=null)
        {
            _playerMining.CanMine = true;
            _target.IsTarget(true);
        }
        else
        {
            DisableMining();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Brick"))
        {
            _collide = collision;
            _checkForTarget = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))
        {
            _collide = collision;
            _checkForTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))
        {
            _checkForTarget = false;
            DisableMining();
        }
    }


    void DisableMining()
    {
        if (_target != null)
        {
            _target.IsTarget(false);
            _target = null;
        }
        _playerMining.CanMine = false;
    }

    void ApplyDamage()
    {
        if (_target != null)
        {
            _target.TakeDamage(1);
            feedBack.PlayFeedbacks();

        }
    }

}
