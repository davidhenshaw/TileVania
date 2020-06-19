using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class GameUIController : MonoBehaviour
{
    public static GameUIController instance;

    [Header("Lives")]
    [SerializeField] GameUIElement _livesUI;

    [Header("Cherries")]
    [SerializeField] GameUIElement _cherriesUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Collectable.ItemCollected += OnCollectablePickedUp;
            PlayerController.PlayerDied += OnPlayerDeath;
            GameSession.InventoryChanged += OnInventoryUpdate;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OnInventoryUpdate(GameSession.instance.GetInventory());
    }

    private void OnDestroy()
    {
        Collectable.ItemCollected -= OnCollectablePickedUp;
        PlayerController.PlayerDied -= OnPlayerDeath;
        GameSession.InventoryChanged -= OnInventoryUpdate;
    }

    private void OnPlayerDeath(Vector2 obj)
    {
        _livesUI.ActivateOneShot();
    }

    void OnInventoryUpdate(PlayerInventory inventory)
    {
        _livesUI.UpdateValue(inventory.lives);
        _cherriesUI.UpdateValue(inventory.cherries);
    }

    void OnCollectablePickedUp(Collectable collectable)
    {
        _cherriesUI.ActivateOneShot();
    }
}
