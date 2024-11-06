using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUISlot : MonoBehaviour
{
    [SerializeField] Image _itemImg;
    [SerializeField] TMP_Text _itemCount;

    private Item _slotItem;
    private string _itemName;
    private int _currentCount;

    public Item CurrentItem => _slotItem;
    public string ItemName => _itemName;
    public Sprite ItemSprite => _itemImg.sprite;
    public int CurrentAmount => _currentCount;

    public void SetData(Item item)
    {
        _slotItem = item;
        _itemImg.sprite = item.ItemSprite;
        _itemName = item.ItemName;
    }

    public void SetData(ItemSO itemData)
    {
        _itemImg.sprite = itemData.ItemSprite;
        _itemName = itemData.ItemName;
    }    

    public void UpdateResouceCount(int count)
    {
        _currentCount = count;
        _itemCount.text = $"{count}";
    }
}
