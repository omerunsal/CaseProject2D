using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Unit, IBuilding
{
    public string Name;
    public string Size;
    public Sprite buildingSprite;
    
    public bool isDropped;
    
    void Start()
    {
        isDropped = false;
    }
    
    private void Update()
    {
        if (isDropped == true)
        {
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    public virtual void SetData(BuildingData buildingData)
    {
        Name = buildingData.Name;
        Size = buildingData.Size;
        buildingSprite = buildingData.BuildingIcon;
    }
}