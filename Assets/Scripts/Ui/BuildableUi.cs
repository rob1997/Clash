using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildableUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _buildablePrefab;
    
    private IBuildable _buildableInstance;
    private Outline _buildableInstanceOutline;
    
    private Vector3 _initialPosition;
    private Vector3 _offset;

    private RectTransform _menuRectT;

    private BuildableManager _buildableManager;
    
    private CameraController _cameraController;

    private bool _canBuild;
    
    public void Initialize(RectTransform menuRectT)
    {
        _menuRectT = menuRectT;

        GameManager.Instance.GetManager(out _buildableManager);
        
        GameManager.Instance.GetController(out _cameraController);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.position;

        _offset = (Vector3) eventData.position - _initialPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = (Vector3) eventData.position - _offset;
        
        Vector3 worldPosition = _cameraController.GetPointerWorldPosition();
        
        if (_buildableInstance != null)
        {
            _buildableInstance.Obj.transform.position = _buildableManager.SnapToGrid(worldPosition);

            bool canBuild = _buildableManager.CanBuild(_buildableInstance.GetData().Size);
            
            if (_canBuild != canBuild)
            {
                if (_buildableInstanceOutline != null) _buildableInstanceOutline.OutlineColor = canBuild ? Color.green : Color.red;
                
                _canBuild = canBuild;
            }
        }

        else
        {
            transform.position = position;

            if (!RectTransformUtility.RectangleContainsScreenPoint(_menuRectT, eventData.position))
            {
                GameObject buildableObj = Instantiate(_buildablePrefab, worldPosition, Quaternion.identity);

                _buildableInstance = buildableObj.GetComponent<IBuildable>();

                _buildableInstanceOutline = buildableObj.GetComponentInChildren<Outline>();
                
                transform.position = _initialPosition;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initialPosition;

        if (_buildableInstance != null)
        {
            string message = string.Empty;
            
            if (_canBuild && _buildableInstance.TryBuild(out message))
            {
                _buildableInstance.Build();
                
                _buildableManager.OccupyGrid(_buildableInstance.GetData().OccupiedTile);
            }

            else
            {
                Destroy(_buildableInstance.Obj);
            }
            
            if (!string.IsNullOrWhiteSpace(message)) Debug.Log(message);

            _buildableInstance = null;
            _buildableInstanceOutline = null;
        }
    }
}
