
using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<BuildingData> SpawnBuilding;
    public static Action<Soldier> SpawnSoldier;
    
    public static Action<bool> ShowInfoPanel;
    public static Action<Building> SetInfoPanel;
}
