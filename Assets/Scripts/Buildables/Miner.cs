using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Buildable<MinerData>
{
    private bool _mining = true;

    [field: SerializeField] public ResourceManager.ResourceType ResourceType { get; private set; }

    private float _depositInterval;

    protected override void OnReadyUpdate()
    {
        base.OnReadyUpdate();
        
        if (!_mining)
        {
            return;
        }

        _depositInterval += Time.deltaTime;

        if (_depositInterval >= Data.DepositInterval)
        {
            _depositInterval = 0;

            float rejected = ResourceManager.Deposit(ResourceType, Data.SingleMinedAmount);
        }
    }
}
