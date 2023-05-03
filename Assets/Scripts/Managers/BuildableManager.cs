using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildableManager : Manager
{
    [field: SerializeField] public Transform BuildableContainer { get; private set; }
    
    [field: Header("Grid System")]
    
    [field: SerializeField] public GridLayout GridLayout { get; private set; }
    
    [field: SerializeField] public Grid FloorGrid { get; private set; }
    
    [field: SerializeField] public Tilemap FloorMap { get; private set; }
    
    [field: SerializeField] public TileBase OccupiedTileBase { get; private set; }

    public IBuildable[] AllBuildables { get; private set; } = { };

    private BoundsInt[] _occupiedCellBounds = { };

    private Vector3Int _cachedCellPosition;
    
    private Vector3Int _cachedSize;

    private BoundsInt _cachedBounds;
    
    public Vector3 SnapToGrid(Vector3 position)
    {
        _cachedCellPosition = GetCellPosition(position);
        
        position = FloorGrid.GetCellCenterWorld(_cachedCellPosition);

        return position;
    }

    public void AddBuildable(IBuildable buildable)
    {
        AllBuildables = AllBuildables.Append(buildable).ToArray();
        
        Debug.Log($"added {buildable.GetData().Title} buildable");
    }
    
    public bool CanBuild(Vector2Int size)
    {
        _cachedSize = To3D(size);
        
        _cachedBounds = new BoundsInt(_cachedCellPosition, _cachedSize);

        foreach (Vector3Int pointInBound in _cachedBounds.allPositionsWithin)
        {
            bool isInBounds = 
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(0, 0, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(0, 1, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(1, 0, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(1, 1, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(0, - 1, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(- 1, 0, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(- 1, - 1, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(1, - 1, 0) * _cachedSize))
                ||
                _occupiedCellBounds.Any(c => c.Contains(pointInBound - new Vector3Int(- 1, 1, 0) * _cachedSize));

            if (isInBounds)
            {
                return false;
            }
        }
        
        return true;
    }

    public void OccupyGrid(TileBase tileBase = null)
    {
        if (tileBase == null) tileBase = OccupiedTileBase;
        
        foreach (Vector3Int pointInBound in _cachedBounds.allPositionsWithin)
        {
            FloorMap.SetTile(pointInBound - new Vector3Int(0, 0, 0) * _cachedSize, tileBase);
            FloorMap.SetTile(pointInBound - new Vector3Int(0, 1, 0) * _cachedSize, tileBase);
            FloorMap.SetTile(pointInBound - new Vector3Int(1, 0, 0) * _cachedSize, tileBase);
            FloorMap.SetTile(pointInBound - new Vector3Int(1, 1, 0) * _cachedSize, tileBase);
        }
        
        _occupiedCellBounds = _occupiedCellBounds.Append(_cachedBounds).ToArray();
    }

    private Vector3Int GetCellPosition(Vector3 position)
    {
        return GridLayout.WorldToCell(position);
    }
    
    private Vector3Int To3D(Vector2Int size)
    {
        Vector3Int size3D = (Vector3Int) size;
        size3D.z = 1;

        return size3D;
    }
}
