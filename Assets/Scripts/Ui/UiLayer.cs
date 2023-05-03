using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLayer : MonoBehaviour
{
    public enum LayerType
    {
        Main,
        Alert
    }
    
    public UiRegion[] Regions { get; private set; }

    public void Initialize()
    {
        Regions = GetComponentsInChildren<UiRegion>();

        foreach (UiRegion region in Regions)
        {
            region.Initialize(this);
        }
    }
}
