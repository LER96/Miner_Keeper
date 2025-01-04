using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumHandler;

public class Brick : MonoBehaviour
{
    [SerializeField] protected int _brickHp;
    [SerializeField] protected List<GameObject> _brickStages = new List<GameObject>();

    protected bool _isTarget;
    protected bool _canCollect;
    protected int _startHp;

    public int HP=> _brickHp;

    protected virtual void Start()
    {
        _canCollect = true;
        _startHp = _brickHp;
        //_brickHp = _brickStages.Count;
        HideAllVariants(_brickHp-1);
    }

    public virtual void TakeDamage(int dmg)
    {
        _brickHp -= dmg;
        if (_brickHp > 0)
        {
            if (_brickHp > _brickStages.Count)
                return;
            HideAllVariants(_brickHp-1);
        }
        else if(_brickHp == 0)
        {
            ResetData();
            this.gameObject.SetActive(false);
        }
    }

    protected void HideAllVariants(int index)
    {
        for (int i = 0; i < _brickStages.Count; i++)
        {
            _brickStages[i].SetActive(false);
        }

        _brickStages[index].SetActive(true);
    }

    public void IsTarget(bool targeted)
    {
        _isTarget = targeted;
    }

    protected virtual void ResetData()
    {
        _brickHp = _startHp;
    }
}
