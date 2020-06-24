using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHealth
{
    event Action Died;

    void TakeDamage(int damage);
    void RestoreHealth(int health);
}
