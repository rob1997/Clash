using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RangedCharacterData), menuName = "Clash Data/Characters/Ranged", order = 0)]
public class RangedCharacterData : CharacterData
{
    [field: SerializeField] public Projectile Projectile { get; private set; }
    
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
    
    [field: SerializeField] public float ProjectileProximity { get; private set; }
}
