using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingData
{
    public string Name;
    public string Size;
    public Sprite BuildingIcon;
    public Building BuildingPrefab;
    public ScriptableBuildingSoldier BuildingSoldierData;
}
