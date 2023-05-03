using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiRoot : MonoBehaviour
{
    [field: SerializeField] public UiElement[] UiElements { get; private set; } = { };
    
    [field: SerializeField] public UiLayer[] UiLayers { get; private set; } = { };

    public bool IsInitialized { get; private set; } = false;
    
    public void Initialize()
    {
        UiLayers = GetComponentsInChildren<UiLayer>();
        
        foreach (UiLayer uiLayer in UiLayers)
        {
            uiLayer.Initialize();
        }
        
        IsInitialized = true;
    }

    public bool GetUiElement<T>(out T element) where T : UiElement
    {
        element = (T) UiElements.FirstOrDefault(u => u is T);

        return element != null;
    }

    public void AddUiElement(UiElement element)
    {
        if (UiElements.Any(u => u.GetType() == element.GetType()))
        {
            Debug.LogError($"{nameof(UiElement)} with type {element.GetType()} already exists");
            
            return;
        }
        
        UiElements = UiElements.Append(element).ToArray();
    }
}
