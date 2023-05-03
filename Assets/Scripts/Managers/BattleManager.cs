using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Manager
{
    [field: SerializeField] public LayerMask CharacterLayer { get; private set; }
    
    [field: SerializeField] public LayerMask BuildableLayer { get; private set; }

    [field: SerializeField] public Transform CharacterContainer { get; private set; }
    
    [field: SerializeField] public float UnitSpawnInterval { get; private set; }
}
