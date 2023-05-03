using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Buildable<T> : MonoBehaviour , IBuildable where T : BuildableData
{
    public enum BuildingPhase
    {
        PreBuild,
        UnderConstruction,
        Ready
    }

    #region PhaseChanged

    public delegate void PhaseChanged(BuildingPhase newPhase);

    public event PhaseChanged OnPhaseChanged;

    private void InvokePhaseChanged(BuildingPhase newPhase)
    {
        OnPhaseChanged?.Invoke(newPhase);
    }

    #endregion
    
    [field: SerializeField] public T Data { get; private set; }
    
    [field: SerializeField] public bool BuildOnStart { get; private set; }
    
    [field: SerializeField] public GameObject[] ConstructionPhases { get; private set; }
    
    public BuildingPhase Phase { get; private set; } = BuildingPhase.PreBuild;

    public GameObject Obj => gameObject;
    
    protected ResourceManager ResourceManager;
    
    private float _duration;
    
    private int _constructionPhaseIndex;
    
    private BuildableManager _buildableManager;
    
    private Collider _collider;

    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    protected virtual void Start()
    {
        GameManager.Instance.GetManager(out _buildableManager);
        
        GameManager.Instance.GetManager(out ResourceManager);
        
        _collider.enabled = false;

        if (BuildOnStart)
        {
            transform.position = _buildableManager.SnapToGrid(transform.position);
            
            bool canBuild = _buildableManager.CanBuild(Data.Size);

            string message = string.Empty;
            
            if (canBuild && TryBuild(out message))
            {
                Build();
                
                _buildableManager.OccupyGrid(Data.OccupiedTile);
            }
            
            Debug.Log(message);
        }
    }

    protected virtual void Update()
    {
        switch (Phase)
        {
            case BuildingPhase.PreBuild:
                break;
            case BuildingPhase.UnderConstruction:
                Construct();
                break;
            case BuildingPhase.Ready:
                OnReadyUpdate();
                break;
        }
    }

    public void Build()
    {
        _buildableManager.AddBuildable(this);
        
        transform.SetParent(_buildableManager.BuildableContainer);
        
        _collider.enabled = true;

        _constructionPhaseIndex = 0;
        _duration = 0;
        
        ToggleConstructionPhase(_constructionPhaseIndex);
        
        ChangePhase(BuildingPhase.UnderConstruction);
    }

    public bool IsNull()
    {
        return this == null || gameObject == null;
    }

    private void Construct()
    {
        _duration += Time.deltaTime;
        float progress = _duration / Data.Duration;
                
        if (progress >= 1f)
        {
            ToggleConstructionPhase(ConstructionPhases.Length - 1);
         
            Debug.Log($"{Data.Title} finished building");
            
            ChangePhase(BuildingPhase.Ready);
                    
            return;
        }

        int phaseIndex = (int) (progress * ConstructionPhases.Length);
                
        if (phaseIndex != _constructionPhaseIndex)
        {
            _constructionPhaseIndex = phaseIndex;
            ToggleConstructionPhase(_constructionPhaseIndex);
        }
    }

    protected virtual void OnReadyUpdate()
    {
        
    }

    public BuildableData GetData()
    {
        return Data;
    }

    public bool TryBuild(out string message)
    {
        message = string.Empty;

        if (Phase != BuildingPhase.PreBuild)
        {
            message = $"can't build, buildable in {Phase} phase";
            
            return false;
        }

        if (!BuildOnStart)
        {
            ResourceManager.Cost cost = Data.Cost;
        
            if (cost.Amount > ResourceManager.GetTotalResourceAmount(cost.ResourceType))
            {
                message = $"can't build, mine more {cost.ResourceType}";
            
                return false;
            }

            ResourceManager.Withdraw(cost.ResourceType, cost.Amount);
        }

        message = $"building {Data.Title}...";
        
        return true;
    }

    private void ChangePhase(BuildingPhase newPhase)
    {
        if (newPhase == Phase) return;

        Phase = newPhase;
        
        InvokePhaseChanged(newPhase);
    }

    private void ToggleConstructionPhase(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        index = Mathf.Clamp(index, 0, ConstructionPhases.Length);
        
        ConstructionPhases[index].SetActive(true);
    }
}
