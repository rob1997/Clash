using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BuildableData : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    
    [field: TextArea]
    [field: SerializeField] public string Description { get; private set; }
    
    [field: SerializeField] public float Duration { get; private set; }
    
    [field: SerializeField] public Vector2Int Size { get; private set; }
    
    [field: SerializeField] public ResourceManager.Cost Cost { get; private set; }
    
    [field: SerializeField] public TileBase OccupiedTile { get; private set; }
}
