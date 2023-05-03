using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : Manager
{
    public enum ResourceType
    {
        Gold,
        Elixir
    }
    
    [Serializable]
    public struct Cost
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        
        [field: SerializeField] public float Amount { get; private set; }

        public void AddAmount(float amountToAdd)
        {
            Amount += amountToAdd;
        }
    }

    #region ResourceModified

    public delegate void ResourceModified(ResourceType type, float amount);

    public event ResourceModified OnResourceModified;

    private void InvokeResourceModified(ResourceType type, float amount)
    {
        OnResourceModified?.Invoke(type, amount);
    }

    #endregion

    [field: SerializeField] public Cost[] StartWithAmounts { get; private set; }
    
    private Storage[] _repository = {};
    
    public void AddStorage(Storage storage)
    {
        _repository = _repository.Append(storage).ToArray();

        Debug.Log($"added {storage.Data.Title} to repository");
        
        int index = 0;
        
        //add start with amount
        foreach (var cost in StartWithAmounts)
        {
            if (cost.Amount > 0)
            {
                float rejected = Deposit(cost.ResourceType, cost.Amount);

                float deposited = cost.Amount - rejected;
                
                cost.AddAmount(- deposited);

                StartWithAmounts[index] = cost;
            }

            index++;
        }
    }

    public float Deposit(ResourceType type, float amount)
    {
        float initialAmount = amount;
        
        float rejected = 0;
        
        float deposited = 0;
        
        foreach (Storage storage in _repository.Where(s => s.ResourceType == type))
        {
            rejected = storage.Deposit(amount);

            deposited += amount - rejected;
            
            if (rejected <= 0)
            {
                break;
            }

            amount = rejected;
        }

        InvokeResourceModified(type, deposited);
        
        Debug.Log($"deposited {deposited} {type}");
        
        return initialAmount - deposited;
    }
    
    public float Withdraw(ResourceType type, float amount)
    {
        float initialAmount = amount;
        
        float rejected = 0;
        
        float withdrawn = 0;
        
        foreach (Storage storage in _repository.Where(s => s.ResourceType == type))
        {
            rejected = storage.Withdraw(amount);

            withdrawn += amount - rejected;
            
            if (rejected <= 0)
            {
                break;
            }

            amount = rejected;
        }

        InvokeResourceModified(type, - withdrawn);
        
        Debug.Log($"withdrawn {withdrawn} {type}");
        
        return initialAmount - withdrawn;
    }

    public float GetTotalResourceAmount(ResourceType type)
    {
        return _repository.Where(s => s.ResourceType == type).Sum(s => s.Amount);
    }
}
