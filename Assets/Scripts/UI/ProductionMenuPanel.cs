using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuPanel : MonoBehaviour
{
    [SerializeField] private ScriptableBuilding scriptableBuilding;
    
    [SerializeField] private RectTransform buildingSelectionButtonParent;
    [SerializeField] private BuildingSelectionButton buildingSelectionButton;
    [SerializeField] private List<BuildingSelectionButton> buildingSelectionButtonList;


    public void SetProductionMenuPanel()
    {
        var buildingDataList = scriptableBuilding.BuildingDataList;

        for (int i = 0; i < buildingDataList.Count; i++)
        {
            BuildingSelectionButton bsb;

            bsb = Instantiate(buildingSelectionButton, buildingSelectionButtonParent);
            bsb.Set(buildingDataList[i]);
            buildingSelectionButtonList.Add(bsb);
        }
    }
}
