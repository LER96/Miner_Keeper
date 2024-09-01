using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour
{
    public Action OnTileClicked;
    [SerializeField] Button _tileButton;
    [SerializeField] Image _tileImage;

    [Header("TilesConnected")]
    [SerializeField] Tile _tileChild;
    [SerializeField] Tile _tileParent;

    Sprite _tileSprite;
    float _points;

    public Tile Parent { get=> _tileParent; set=> _tileParent = value; }
    public Tile Neighbor { get => _tileChild; set => _tileChild = value; }


    private void Start()
    {
        _tileButton.onClick.AddListener(ButtonPress);

        //if (_tileParent != null)
        //    OnTileClicked += _tileParent.OnChildTaken;
        //if (_tileChild != null)
        //    OnTileClicked += _tileChild.OnParentTaken;
        
    }

    public void SetSO(TileSO tileSO)
    {
        _tileSprite= tileSO.TileSprite;
        _tileImage.sprite= _tileSprite;
        _points= tileSO.TileScore;
    }

    void ButtonPress()
    {
        OnTileClicked.Invoke();
    }

    public void OnChildTaken()
    {
        if(_tileChild != null)
            _tileChild = null;
    }

    public void OnParentTaken()
    {
        if(_tileParent != null)
            _tileParent= null;
    }

}
