using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Buildable<StorageData>
{
    #region Withdrawal

    public delegate void Withdrawal(float amount);

    public event Withdrawal OnWithdrawal;

    private void InvokeWithdrawal(float amount)
    {
        OnWithdrawal?.Invoke(amount);
    }

    #endregion

    #region Deposited

    public delegate void Deposited(float amount);

    public event Deposited OnDeposited;

    private void InvokeDeposited(float amount)
    {
        OnDeposited?.Invoke(amount);
    }

    #endregion

    public float Amount { get; private set; }

    [field: SerializeField] public ResourceManager.ResourceType ResourceType { get; private set; }

    protected override void Start()
    {
        base.Start();

        OnPhaseChanged += phase =>
        {
            if (phase == BuildingPhase.Ready)
            {
                ResourceManager.AddStorage(this);
            }
        };
    }

    public float Withdraw(float amount)
    {
        float withdrawn = Mathf.Clamp(amount, 0f, Amount);

        Amount -= withdrawn;
        
        InvokeWithdrawal(withdrawn);

        float rejected = amount - withdrawn;
        
        return rejected;
    }
    
    public float Deposit(float amount)
    {
        float deposited = Mathf.Clamp(amount, 0f, Data.MaxAmount - Amount);

        Amount += deposited;
        
        InvokeDeposited(deposited);
        
        float rejected = amount - deposited;
        
        Debug.Log($"deposited {amount} {ResourceType} at storage");
        
        return rejected;
    }
}
