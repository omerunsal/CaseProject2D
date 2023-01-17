using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonNameText;
    [SerializeField] private TextMeshProUGUI buttonSizeText;

    public void Set(BuildingData buildingData)
    {
        buttonNameText.text = buildingData.Name;
        buttonSizeText.text = buildingData.Size;
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameEvents.SpawnBuilding?.Invoke(buildingData);
        });
    }
}
