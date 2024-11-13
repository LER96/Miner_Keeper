using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] PlayerSO _playerData;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerMining playerMining;
    [SerializeField] Inventory inventory;

    private float _maxHp;
    private float _currentHp;

    public float MaxHP => _maxHp;
    public float CurrentHP { get => _currentHp; set => _currentHp = value; }
    public PlayerSO PlayerData => _playerData;
    public PlayerMovement MovementScript => playerMovement;
    public PlayerMining MiningScript => playerMining;
    public Inventory PlayerInventory => inventory;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMining = GetComponent<PlayerMining>();
        inventory = GetComponent<Inventory>();
        SetData();
        _currentHp = _maxHp;

        PlayerManger.Instance.PlayerHandler = this;
    }

    public void SetDataLevel(PlayerSO data)
    {
        _playerData = data;
        SetData();
    }

    void SetData()
    {
        _maxHp = _playerData.HP;
        playerMovement.MovementSpeed = _playerData.MovementSpeed;
        playerMining.MiningRate = _playerData.MiningRate;

    }
}
