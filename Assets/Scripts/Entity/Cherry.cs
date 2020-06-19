using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;


public class Cherry : Collectable
{
    [SerializeField] AudioClip _pickUpSound;

    public override void OnCollected()
    {
        //Play Sound
        SFX.instance.Play(_pickUpSound, 0.8f);
        //EazySoundManager.PlaySound(_pickUpSound);

        //Delete self
        Destroy(gameObject);
    }

    public override void UpdateInventory(PlayerInventory inventory)
    {
        inventory.cherries = inventory.cherries + 1;
        GameSession.instance.SetInventory(inventory);
    }
}
