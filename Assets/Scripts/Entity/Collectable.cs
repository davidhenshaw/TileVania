using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Collectable : MonoBehaviour, IUpdateInventory
{
    public static event Action<Collectable> ItemCollected;

    // Prevents the ItemCollected function from running multiple times
    // due to the player having multiple colliders
    bool _collected = false;

    public abstract void OnCollected();
    public abstract void UpdateInventory(PlayerInventory inventory);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected)
            return;

        //if the player touches me, I am collected
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player != null)
        {
            _collected = true;
            ItemCollected?.Invoke(this);
            OnCollected();
        }
    }


}
