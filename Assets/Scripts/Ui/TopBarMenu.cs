using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarMenu : UiElement
{
    [SerializeField] private TextMeshProUGUI _goldValueText;
    [SerializeField] private TextMeshProUGUI _elixirValueText;

    private ResourceManager _resourceManager;

    public override void Initialize(UiRegion uiRegion)
    {
        base.Initialize(uiRegion);
        
        if (GameManager.Instance.GetManager(out _resourceManager))
        {
            _resourceManager.OnResourceModified += (type, amount) =>
            {
                OnResourceModified(type); 
            };
            
            Debug.Log($"{nameof(TopBarMenu)} resource listeners added");
            
            //initialize manually too
            foreach (ResourceManager.ResourceType type in Enum.GetValues(typeof(ResourceManager.ResourceType)))
            {
                OnResourceModified(type);
            }
        }
    }

    private void OnResourceModified(ResourceManager.ResourceType type)
    {
        float totalAmount = _resourceManager.GetTotalResourceAmount(type);
                
        switch (type)
        {
            case ResourceManager.ResourceType.Gold:
                _goldValueText.text = $"{totalAmount}";
                break;
            case ResourceManager.ResourceType.Elixir:
                _elixirValueText.text = $"{totalAmount}";
                break;
        }
    }
}
