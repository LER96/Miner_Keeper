using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUISlot : MonoBehaviour
{
    [SerializeField] Image _itemImg;
    [SerializeField] TMP_Text _itemCount;

    public Item CurrentItem => _slotItem;

    private Item _slotItem;
    private int _currentCount;
    public void SetData(Item item)
    {
        _slotItem = item;
        _itemImg.sprite = item.ItemSprite;
    }

    public void UpdateResouceCount(int count)
    {
        _currentCount = count;
        _itemCount.text = $"{count}";
    }
}
