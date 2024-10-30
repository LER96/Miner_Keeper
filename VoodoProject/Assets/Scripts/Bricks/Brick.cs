using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumHandler;

public class Brick : MonoBehaviour
{
    [SerializeField] int _brickHp;
    [SerializeField] List<GameObject> _brickStages = new List<GameObject>();
    [SerializeField] Item item;
    [SerializeField] ParticleSystem _hitvfx;
    [SerializeField] BrickType _brickType;

    bool _canCollect;
    Inventory _inventory;

    private void Start()
    {
        _canCollect = true;
        _inventory = PlayerManger.Instance.PlayerInventory;
        HideAllVariants(_brickHp-1);
    }

    public void TakeDamage(int dmg)
    {
        _brickHp -= dmg;
        if (_brickHp > 0)
        {
            _hitvfx.Play();
            if (_brickHp > _brickStages.Count)
                return;
            HideAllVariants(_brickHp-1);
        }
        else if(_brickHp == 0 && _brickType == BrickType.Break)
        {
            this.gameObject.SetActive(false);
        }
        else if(_brickType == BrickType.Resource && _canCollect)
        {
            _canCollect = false;
            if (item != null)
            {
                _inventory.AddItem(item);
            }
        }
    }

    void HideAllVariants(int index)
    {
        for (int i = 0; i < _brickStages.Count; i++)
        {
            _brickStages[i].SetActive(false);
        }

        _brickStages[index].SetActive(true);
    }
}
