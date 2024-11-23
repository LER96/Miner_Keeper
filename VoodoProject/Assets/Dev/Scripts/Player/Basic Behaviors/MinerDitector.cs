using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerDitector : MonoBehaviour
{
    [SerializeField] Brick _target;
    [SerializeField] LayerMask _layer;
    private PlayerMining _playerMining;

    private void Start()
    {
        _playerMining = PlayerManger.Instance.MiningScript;
        _playerMining.OnHit += ApplyDamage;
    }

    private void Update()
    {
        CheckForTarget();
    }

    protected virtual void CheckForTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), transform.localScale.x, _layer);
        if(colliders.Length>0)
        {
            _playerMining.CanMine = true;
            _target = colliders[0].GetComponent<Brick>();
            _target.IsTarget(true);
        }
        else
        {
            if (_target != null)
            {
                _target.IsTarget(false);
                _target = null;
            }
            _playerMining.CanMine = false;
        }
    }

    void ApplyDamage()
    {
        if(_target!=null)
        {
            _target.TakeDamage(1);
        }
    }

}
