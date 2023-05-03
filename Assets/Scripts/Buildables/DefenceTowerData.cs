using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DefenceTowerData), menuName = "Clash Data/Buildable/Defence Tower", order = 0)]
public class DefenceTowerData : BuildableData
{
    [field: SerializeField] public float Range { get; private set; }
    
    [field: SerializeField] public int Burst { get; private set; }
    
    [field: SerializeField] public float Proximity { get; private set; }
    
    [field: SerializeField] public Projectile Projectile { get; private set; }
    
    [field: SerializeField] public float Damage { get; private set; }
    
    [field: SerializeField] public float FireInterval { get; private set; }
    
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
}
