using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerDitector : MonoBehaviour
{
    [SerializeField] private Brick _target;
    private PlayerMining _playerMining;
    private bool _canMine;

    private void Start()
    {
        _playerMining = PlayerManger.Instance.MiningScript;
        _playerMining.OnHit += ApplyDamage;
    }

    public void ApplyDamage()
    {
        if(_target!=null)
        {
            _target.TakeDamage(1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick")&& _target==null)
        {
            _playerMining.CanMine = true;
            _target = collision.GetComponent<Brick>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerMining.CanMine = false;
        _target = null;
    }

}
