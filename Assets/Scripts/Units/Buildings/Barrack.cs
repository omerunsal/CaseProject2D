using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Barrack : Building, IProductive
{
    [SerializeField] private List<Soldier> _soldierPrefabList;
    public List<Soldier> SoldierPrefabList
    {
        get => _soldierPrefabList;
        set => _soldierPrefabList = value;
    }

    public override void SetData(BuildingData buildingData)
    {
        base.SetData(buildingData);
        SoldierPrefabList = buildingData.BuildingSoldierData.SoldierList;
    }
}