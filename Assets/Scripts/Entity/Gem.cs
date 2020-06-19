using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{
    public override void OnCollected()
    {
        //Play sound

        //Delete self
    }

    public override void UpdateInventory(PlayerInventory inventory)
    {
        inventory.gems++;
    }
}
