using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BarrackData), menuName = "Clash Data/Buildable/Barrack", order = 0)]
public class BarrackData : BuildableData
{
    [field: SerializeField] public CharacterData[] Characters { get; private set; }
}
