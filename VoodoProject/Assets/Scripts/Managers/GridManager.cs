using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Variables")]
    [SerializeField] int _numberOfXTiles;
    [SerializeField] float _tileGap;

    [Header("References")]
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] GameObject _spawnPointPreFab;
    [SerializeField] List<TileSO> tilesData=new List<TileSO>();

    [Header("Spawn Parent")]
    [SerializeField] RectTransform _spawnPointsParent;
    [SerializeField] GridLayoutGroup _gridLayoutGroup;

    List<Transform> spawnPoints= new List<Transform>();
    List<Tile> tiles = new List<Tile>();

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        CreateMap();
        _gridLayoutGroup.spacing = new Vector2(_tileGap,0);
    }

    public void CreateMap()
    {
        for (int i = 0; i < _numberOfXTiles; i++)
        {
            Transform spawnPoint = Instantiate(_spawnPointPreFab, _spawnPointsParent).transform;
            spawnPoints.Add(spawnPoint);
        }
    }

    public void CreateTiles()
    {

    }
}
