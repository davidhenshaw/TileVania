using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] int _currHealth;
    [SerializeField] int _maxHealth;
    bool _isDead;

    public event Action DamageTaken;
    public event Action HealthRestored;
    public event Action Died;

    public void RestoreHealth(int health)
    {
        _currHealth = Mathf.Clamp(_currHealth + Mathf.Abs(health), 0, _maxHealth);
        HealthRestored?.Invoke();

        Debug.Log(health + " health Restored: " + _currHealth);
    }

    public void TakeDamage(int damage)
    {
        _currHealth = Mathf.Clamp(_currHealth - Mathf.Abs(damage), 0, _maxHealth);
        DamageTaken?.Invoke();

        Debug.Log("Took " + damage + " damage: " + _currHealth);

        if (_currHealth <= 0 && !_isDead)
        {
            Died?.Invoke();
            _isDead = true;
        }

    }
}
