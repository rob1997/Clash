using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Manager
{
    [field: SerializeField] public UiRoot UiRoot { get; private set; }

    private void Start()
    {
        UiRoot.Initialize();
    }
}
