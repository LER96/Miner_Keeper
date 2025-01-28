using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBrick : Brick
{
    [SerializeField] protected ItemSO _itemData;
    [SerializeField] float _reviveDelay;

    private float _copyReviveDelay;
    int currentStage;
    float _currentTime;

    protected override void Start()
    {
        base.Start();
        _copyReviveDelay = _reviveDelay;
        currentStage = 1;
        HideAllVariants(_brickHp);
    }

    private void Update()
    {
        if (_canCollect == false)// || (_isTarget==false && _brickHp< _startHp))
        {
            ReviveDelayTimer();
        }
    }

    public override void TakeDamage(int dmg)
    {
        _brickHp -= dmg;
        if (_brickHp > 0)
        {
            if (_brickHp > _brickStages.Count)
                return;

            Collect();
            HideAllVariants(_brickHp);
        }
        else if (_brickHp == 0&& _canCollect)
        {
            Collect();
            HideAllVariants(0);
            _canCollect = false;
        }
    }

    void Collect()
    {
        UpgradeManager.Instance.UpgradeHandler.DropItem(_itemData, this.transform);
    }

    void ReviveDelayTimer()
    {
        _reviveDelay -= Time.deltaTime;
        if (_reviveDelay <= 0)
        {
            _brickHp = _startHp;
            _reviveDelay = _copyReviveDelay;
            _canCollect = true;
            HideAllVariants(_brickHp);
        }
    }

    protected override void ResetData()
    {
        base.ResetData();
        currentStage = 1;
        _currentTime = 0;
        _canCollect = true;
    }
}
