using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoldierSelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soldierNameText;
    [SerializeField] private Image soldierImage;
   
    public void Set(Soldier soldier)
    {
        soldierNameText.text = soldier.SoldierData.Name;
        soldierImage.sprite = soldier.SoldierData.SoldierIcon;
        
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameEvents.SpawnSoldier?.Invoke(soldier);
        });
    }
}
