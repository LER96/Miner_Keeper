using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumHandler;

public class PlayerMining : MonoBehaviour
{
    public event Action OnHit;
    [SerializeField] Animator _animator;
    [SerializeField] float _miningRate;

    bool _canMine;
    bool _alreadyMine;
    float _currentMiningTime;

    public bool CanMine { set => _canMine = value; }

    private void Start()
    {
        _currentMiningTime = _miningRate;
    }

    private void Update()
    {
        if (_canMine)
        {
            MiningRate();
        }
        else
            _currentMiningTime = 0;
    }

    void MiningRate()
    {
        if(_currentMiningTime < _miningRate && _alreadyMine==false)
        {
            _currentMiningTime += Time.deltaTime;
        }
        else
        {
            _alreadyMine = true;
            CheckDirrection();
            _currentMiningTime = 0;
        }
    }

    void CheckDirrection()
    {
        PlayerDirrection dirrection = PlayerManger.Instance.MovementScript.PlayerDirrection;
        switch(dirrection)
        {
            case PlayerDirrection.Side:
                _animator.Play("SideDig");
                break;
            case PlayerDirrection.Up:
                _animator.Play("UpDig");
                break;
            case PlayerDirrection.Dowm:
                _animator.Play("DownDig");
                break;

        }
    }

    public void Hit()
    {
        _alreadyMine = false;
        OnHit?.Invoke();
    }
}
