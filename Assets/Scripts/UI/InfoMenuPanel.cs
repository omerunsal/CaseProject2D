using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenuPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private Image buildingImage;

    [SerializeField] private TextMeshProUGUI productionText;
    [SerializeField] private Image soldierPanel;


    [SerializeField] private RectTransform soldierSelectionButtonParent;
    [SerializeField] private SoldierSelectionButton soldierSelectionButton;
    [SerializeField] private List<SoldierSelectionButton> soldierSelectionButtonList;

    private void OnEnable()
    {
        GameEvents.SetInfoPanel += SetInfoPanel;
    }

    private void OnDisable()
    {
        GameEvents.SetInfoPanel -= SetInfoPanel;
    }

    private void SetInfoPanel(Building building)
    {
        buildingNameText.text = building.Name;
        buildingImage.sprite = building.buildingSprite;

        bool isProductive = building.TryGetComponent(out IProductive iProductive);
        if (isProductive)
        {
            var soldierList = iProductive.SoldierPrefabList;

            for (int i = 0; i < soldierList.Count; i++)
            {
                SoldierSelectionButton ssb;

                if (i < soldierSelectionButtonList.Count)
                {
                    ssb = soldierSelectionButtonList[i];
                }
                else
                {
                    ssb = Instantiate(soldierSelectionButton, soldierSelectionButtonParent);
                    soldierSelectionButtonList.Add(ssb);
                }

                ssb.Set(soldierList[i]);
            }

            SetProductivePanel(true);
        }
        else
        {
            SetProductivePanel(false);
        }
    }

    private void SetProductivePanel(bool state)
    {
        productionText.gameObject.SetActive(state);
        soldierPanel.gameObject.SetActive(state);
    }
}