using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileSO : ScriptableObject
{
    [SerializeField] Sprite _tileSprite;
    [SerializeField] float _tileScore;

    public Sprite TileSprite => _tileSprite;
    public float TileScore => _tileScore;
}
