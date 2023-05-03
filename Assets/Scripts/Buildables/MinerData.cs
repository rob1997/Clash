using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MinerData), menuName = "Clash Data/Buildable/Miner", order = 0)]
public class MinerData : BuildableData
{
    [field: SerializeField] public float SingleMinedAmount { get; private set; }
    
    [field: SerializeField] public float DepositInterval { get; private set; }
}
