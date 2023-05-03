using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRegion : MonoBehaviour
{
    public enum RegionType
    {
        TopBar,
        BottomBar,
        Main
    }

    public UiElement[] UiElements { get; private set; }
    
    public UiElement LoadedUiElement { get; private set; }

    public UiLayer UiLayer { get; private set; }
    
    public void Initialize(UiLayer uiLayer)
    {
        UiLayer = uiLayer;

        UiElements = GetComponentsInChildren<UiElement>();
        
        foreach (UiElement uiElement in UiElements)
        {
            uiElement.Initialize(this);
        }
    }

    public void Load(UiElement uiElement)
    {
        if (LoadedUiElement != null)
        {
            LoadedUiElement.Unloaded();
        }
        
        LoadedUiElement = uiElement;
        
        LoadedUiElement.Loaded();
    }
    
    public void Unload()
    {
        if (LoadedUiElement != null)
        {
            LoadedUiElement.Unloaded();
        }
        
        LoadedUiElement = null;
    }
}
