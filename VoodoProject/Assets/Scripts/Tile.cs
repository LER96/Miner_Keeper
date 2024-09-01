using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour
{

    [SerializeField] TileSO _tileSo;
    [SerializeField] Button _tileButton;

    private void Start()
    {
        _tileButton.onClick.AddListener(ButtonPress);
    }

    void ButtonPress()
    {

    }

    void ButtonRelease()
    {

    }
}
