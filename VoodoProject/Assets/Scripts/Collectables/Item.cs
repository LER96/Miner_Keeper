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

    public string ItemName => _itemName;
    public Sprite ItemSprite => _itemSprite.sprite;
    public int Value => value;


    private void OnEnable()
    {
        SetData();
    }

    private void Start()
    {
        SetData();
    }

    void SetData()
    {
        _itemName = _itemData.ItemName;
        _itemSprite.sprite = _itemData.ItemSprite;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
}
