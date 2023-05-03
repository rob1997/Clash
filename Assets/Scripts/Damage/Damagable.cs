using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    #region DamageTaken

    public delegate void DamageTaken(float damage);

    public event DamageTaken OnDamageTaken;

    private void InvokeDamageTaken(float damage)
    {
        OnDamageTaken?.Invoke(damage);
    }

    #endregion

    #region Killed

    public delegate void Killed(float damage);

    public event Killed OnKilled;

    private void InvokeKilled(float damage)
    {
        OnKilled?.Invoke(damage);
    }

    #endregion
    
    [field: SerializeField] public float Health { get; private set; }
    
    [field: SerializeField] public float FullHealth { get; private set; }

    [field: SerializeField] public Transform Target { get; private set; }

    public void TakeDamage(float damage)
    {
        Health -= Mathf.Clamp(damage, 0, Health);

        Debug.Log($"{damage} damage taken {gameObject.name}");
        
        InvokeDamageTaken(damage);
        
        if (Health <= 0)
        {
            //un-clamped value
            InvokeKilled(damage);
            
            Destroy(gameObject);
        }
    }
}
