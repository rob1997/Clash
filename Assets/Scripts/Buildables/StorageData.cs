using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(StorageData), menuName = "Clash Data/Buildable/Storage", order = 0)]
public class StorageData : BuildableData
{
    [field: SerializeField] public float MaxAmount { get; private set; }
}
