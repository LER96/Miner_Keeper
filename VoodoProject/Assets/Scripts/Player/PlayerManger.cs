using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public static PlayerManger Instance;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerMining playerMining;
    [SerializeField] Inventory inventory;

    public PlayerMovement MovementScript => playerMovement;
    public PlayerMining MiningScript => playerMining;
    public Inventory PlayerInventory => inventory;

    private void Awake()
    {
        Instance = this;

        playerMovement = GetComponent<PlayerMovement>();
        playerMining = GetComponent<PlayerMining>();
        inventory = GetComponent<Inventory>();
    }


}
