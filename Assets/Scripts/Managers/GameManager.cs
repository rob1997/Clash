using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Controls Controls { get; private set; }

    public Camera MainCamera { get; private set; }

    private Manager[] _managers;
    private Controller[] _controllers;
    
    protected override void Awake()
    {
        base.Awake();
        
        Controls = new Controls();
        
        Controls.Enable();
        
        MainCamera = Camera.main;
        
        _managers = GetComponentsInChildren<Manager>();
        
        _controllers = GetComponentsInChildren<Controller>();
    }

    public bool GetManager<T>(out T manager) where T : Manager
    {
        manager = (T) _managers.FirstOrDefault(m => m is T);

        return manager != null;
    }
    
    public bool GetController<T>(out T controller) where T : Controller
    {
        controller = (T) _controllers.FirstOrDefault(c => c is T);

        return controller != null;
    }
}
