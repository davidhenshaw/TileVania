using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Collectable
{
    [SerializeField] AudioClip _pickUpSound;

    public override void OnCollected()
    {
        //Play Sound
        SFX.instance.Play(_pickUpSound, 0.8f);
        //Delete self
        Destroy(gameObject);
    }

    public override void UpdateInventory(PlayerInventory inventory)
    {
        inventory.cherries = inventory.cherries + 1;
        GameSession.instance.SetInventory(inventory);
    }
}
