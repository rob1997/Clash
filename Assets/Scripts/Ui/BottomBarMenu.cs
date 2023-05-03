using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarMenu : UiElement
{
    [SerializeField] private Transform _unitsUiContainer;
    [SerializeField] private Transform _buildableUiContainer;
    
    private BuildableUi[] _buildableUis;

    private void Start()
    {
        _buildableUis = _buildableUiContainer.GetComponentsInChildren<BuildableUi>();

        RectTransform rectTransform = (RectTransform) transform;
        
        foreach (BuildableUi buildableUi in _buildableUis)
        {
            buildableUi.Initialize(rectTransform);
        }
    }
}
