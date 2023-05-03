using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable
{
    GameObject Obj { get; }

    BuildableData GetData();

    bool TryBuild(out string message);
    
    void Build();
    
    bool IsNull();
}
