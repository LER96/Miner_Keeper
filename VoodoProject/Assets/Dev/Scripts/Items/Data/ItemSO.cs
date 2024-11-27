using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] Sprite _itemSprite;
    [SerializeField] string _itemName;
    [SerializeField] int _value;

    public Sprite ItemSprite => _itemSprite;
    public string ItemName => _itemName;
    public int Value => _value;
}
