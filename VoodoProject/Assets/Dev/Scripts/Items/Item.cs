using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemSO _itemData;
    [SerializeField] SpriteRenderer _itemSprite;
    [SerializeField] int value;

    private string _itemName;

    public ItemSO ItemData => _itemData;
    public string ItemName => _itemName;
    public Sprite ItemSprite => _itemSprite.sprite;
    public int Value => value;

    private void OnEnable()
    {
        SetData(_itemData);
    }

    private void Start()
    {
        SetData(_itemData);
    }

    #region Set Data
    public void SetData(ItemSO itemData)
    {
        if (_itemData != null)
        {
            _itemName = itemData.ItemName;
            _itemSprite.sprite = itemData.ItemSprite;
            value = itemData.Value;
        }
    }

    public void SetData(string name, Sprite sprite)
    {
        _itemName = name;
        _itemSprite.sprite = sprite;
    }
    #endregion

    #region Compare Item
    public bool CompareItem(Item item)// by item
    {
        return item.ItemName == _itemName;
    }
    public bool CompareItem(string name) // by variables
    {
        return name == _itemName;
    }
    #endregion

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
}
