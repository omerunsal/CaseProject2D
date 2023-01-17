using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ProductionMenuPanel productionMenuPanel;
    [SerializeField] private InfoMenuPanel infoMenuPanel;

    private void Awake()
    {
        productionMenuPanel.SetProductionMenuPanel();
    }

    private void OnEnable()
    {
        GameEvents.ShowInfoPanel += ShowInfoPanel;
    }

    private void OnDisable()
    {
        GameEvents.ShowInfoPanel -= ShowInfoPanel;
    }

    private void ShowInfoPanel(bool state)
    {
        
        infoMenuPanel.gameObject.SetActive(state);
    }
}
