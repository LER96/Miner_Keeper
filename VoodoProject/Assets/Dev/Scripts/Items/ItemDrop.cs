using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] Image _itemImg;

    private ItemSO _itemData;
    
    public void SetData(ItemSO itemData)
    {
        _itemData = itemData;
        _itemImg.sprite = itemData.ItemSprite;
    }

}
