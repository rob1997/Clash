using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiElement : MonoBehaviour
{
    public UiRegion UiRegion { get; private set; }

    protected UiManager UiManager { get; private set; }
    
    public virtual void Initialize(UiRegion uiRegion)
    {
        UiRegion = uiRegion;

        GameManager.Instance.GetManager(out UiManager uiManager);

        UiManager = uiManager;
        
        UiManager.UiRoot.AddUiElement(this);
    }

    public void Load()
    {
        UiRegion.Load(this);
    }
    
    public void Unload()
    {
        if (UiRegion.LoadedUiElement == this)
        {
            UiRegion.Unload();
        }
    }
    
    public virtual void Loaded()
    {
        gameObject.SetActive(true);
    }
    
    public virtual void Unloaded()
    {
        gameObject.SetActive(false);
    }
}
