using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : Buildable<TownHallData>
{
    public int Level { get; private set; } = 1;
}
