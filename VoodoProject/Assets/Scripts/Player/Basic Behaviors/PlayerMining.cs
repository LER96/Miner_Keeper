using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _miningRate;

    bool _mining;
    float _currentMiningTime;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    void MiningRate()
    {
        if(_currentMiningTime < _miningRate)
        {
            _currentMiningTime += Time.deltaTime;
        }
        else
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Brick"))
        {
            _mining= true;
            _target= other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _target = null;
    }
}
