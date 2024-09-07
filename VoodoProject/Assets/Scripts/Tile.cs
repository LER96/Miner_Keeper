using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    [SerializeField] Button _tileButton;
    [SerializeField] Image _tileImage;

    [Header("TilesConnected")]
    [SerializeField] Tile _tileParent;
    [SerializeField] int tileSpawnIndex;

    Sprite _tileSprite;
    float _points;

    public Tile Parent { get=> _tileParent; set=> _tileParent = value; }

    private void Start()
    {
        _tileButton.onClick.AddListener(ButtonPress);
        
    }

    public void SetSO(TileSO tileSO)
    {
        _tileSprite= tileSO.TileSprite;
        _tileImage.sprite= _tileSprite;
        _points= tileSO.TileScore;
    }

    void ButtonPress()
    {
        if (_tileParent != null)
        {
            _tileParent.OnChildTaken(this.transform);
            _tileParent = null;
        }
    }

    public void OnChildTaken(Transform child)
    {
        transform.DOMove(child.position, 0.5f);
    }

}
