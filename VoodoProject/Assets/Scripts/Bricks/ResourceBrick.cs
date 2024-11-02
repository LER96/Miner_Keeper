using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBrick : Brick
{
    [SerializeField] protected Item item;
    [SerializeField] float _reviveDelay;
    [SerializeField] float _reviveTime;

    int currentStage;
    float timeForEachStage;
    float _currentTime;

    protected override void Start()
    {
        base.Start();
        currentStage = 1;
        timeForEachStage = _reviveTime / _brickStages.Count;
    }

    private void Update()
    {
        if(_canCollect==false)
        {
            Revive();
        }
    }

    public override void TakeDamage(int dmg)
    {
        _brickHp -= dmg;
        if (_brickHp > 0)
        {
            _hitvfx.Play();
            if (_brickHp > _brickStages.Count)
                return;

            Collect();
            HideAllVariants(_brickHp - 1);
        }
        else if (_brickHp == 0&& _canCollect)
        {
            StartCoroutine(ReviveDelay());
        }
    }

    void Collect()
    {
        item.gameObject.SetActive(true);
        if (item != null)
        {
            _inventory.AddItem(item);
            item.transform.DOJump(_inventory.transform.position, 2, 1, 0.3f).OnComplete(HideItem);
        }
    }

    void HideItem()
    {
        if(item!=null)
        {
            item.gameObject.SetActive(false);
        }
    }

    IEnumerator ReviveDelay()
    {
        yield return new WaitForSeconds(_reviveDelay);
        _canCollect = false;
    }

    void Revive()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= timeForEachStage * currentStage && currentStage < _brickStages.Count)
        {
            currentStage++;
            HideAllVariants(currentStage - 1);
        }
        else if (currentStage>=_brickStages.Count)
        {
            ResetData();
        }

    }

    protected override void ResetData()
    {
        base.ResetData();
        currentStage = 1;
        _currentTime = 0;
        _canCollect = true;
        //item.gameObject.SetActive(true);
    }
}
