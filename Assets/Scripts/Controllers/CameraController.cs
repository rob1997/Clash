using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller
{
    [field: SerializeField] public Transform Target { get; private set; }
    
    [field: SerializeField] public float DragSpeed { get; private set; }

    [field: SerializeField] public float MapSize { get; private set; } = 100;
    
    [field: SerializeField] public RectTransform MainRectTransform { get; private set; }

    public bool Dragging => _dragging;
    
    private Controls _controls;

    private bool _dragging;

    private Vector3 _initialTargetPosition;
    
    private Vector3 _lastPointerPosition;
    
    private Camera _mainCamera;

    private BattleManager _battleManager;
    
    private void Start()
    {
        _controls = GameManager.Instance.Controls;

        _controls.Player.Tap.started += context =>
        {
            _lastPointerPosition = GetPointerPosition();

            if (!RectTransformUtility.RectangleContainsScreenPoint(MainRectTransform, _lastPointerPosition))
            {
                return;
            }
            
            _dragging = true;
        };
        
        _controls.Player.Tap.canceled += context => { _dragging = false; };

        _initialTargetPosition = Target.position;

        _mainCamera = GameManager.Instance.MainCamera;

        GameManager.Instance.GetManager(out _battleManager);
    }

    private void Update()
    {
        if (_dragging)
        {
            MoveTarget();
        }
    }

    public void DisableDrag()
    {
        _dragging = false;
    }
    
    private void MoveTarget()
    {
        Vector3 targetPosition = Target.position;
        
        Vector3 pointerPosition = GetPointerPosition();

        Vector3 direction = _mainCamera.ScreenToWorldPoint(pointerPosition) - _mainCamera.ScreenToWorldPoint(_lastPointerPosition);

        _lastPointerPosition = pointerPosition;
        
        Vector3 position = targetPosition - DragSpeed * direction;

        position.x = Mathf.Clamp(position.x, _initialTargetPosition.x - MapSize,
            _initialTargetPosition.x + MapSize);
            
        position.z = Mathf.Clamp(position.z, _initialTargetPosition.z - MapSize,
            _initialTargetPosition.z + MapSize);

        position.y = targetPosition.y;
        
        Target.position = position;
    }

    public Vector3 GetPointerPosition()
    {
        Vector3 pointerPosition = _controls.Player.Pointer.ReadValue<Vector2>();
        
        float distance = Vector3.Distance(Target.position, _mainCamera.transform.position);
        pointerPosition.z = distance;

        return pointerPosition;
    }
    
    public Vector3 GetPointerWorldPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_controls.Player.Pointer.ReadValue<Vector2>());

        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, ~_battleManager.CharacterLayer))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
