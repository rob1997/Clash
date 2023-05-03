using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    
    [field: TextArea]
    [field: SerializeField] public string Description { get; private set; }
    
    [field: SerializeField] public float Damage { get; private set; }
    
    [field: SerializeField] public float AttackInterval { get; private set; }
    
    [field: SerializeField] public float AttackRange { get; private set; }
    
    [field: SerializeField] public float MoveSpeed { get; private set; }
}
