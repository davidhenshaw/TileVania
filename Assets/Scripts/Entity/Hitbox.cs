﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] int _damage;

    public event Action hit;

    public void Enable()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamage damagableObj = collision.GetComponent<ITakeDamage>();

        if(damagableObj != null)
        {
            damagableObj.TakeDamage(_damage);
            hit?.Invoke();
        }
    }
}
