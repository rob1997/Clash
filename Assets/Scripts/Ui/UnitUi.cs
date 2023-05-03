using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    
    private CameraController _cameraController;
    
    private BattleManager _battleManager;
    
    private Controls _controls;
    
    private Toggle _toggle;

    private bool _spawning;
    
    private float _spawnIntervalTime;
    
    private bool _selected;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        _toggle.onValueChanged.AddListener(isOn => { _selected = isOn; });
        
        _controls = GameManager.Instance.Controls;
        
        GameManager.Instance.GetController(out _cameraController);
        
        GameManager.Instance.GetManager(out _battleManager);

        _controls.Player.Tap.started += context =>
        {
            _spawning = _selected && RectTransformUtility.RectangleContainsScreenPoint(_cameraController.MainRectTransform,
                _controls.Player.Pointer.ReadValue<Vector2>());
        };
        
        _controls.Player.Tap.canceled += context =>
        {
            if (_spawning)
            {
                _spawning = false;

                _spawnIntervalTime = 0f;
            }
        };
    }

    private void Update()
    {
        if (_spawning)
        {
            if (_cameraController.Dragging) _cameraController.DisableDrag();
            
            if (_spawnIntervalTime >= _battleManager.UnitSpawnInterval)
            {
                Vector3 position = _cameraController.GetPointerWorldPosition();

                Instantiate(_unitPrefab, position, Quaternion.identity, _battleManager.CharacterContainer);

                _spawnIntervalTime = 0;
            }

            else
            {
                _spawnIntervalTime += Time.deltaTime;
            }
        }
    }
}
