using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExit : MonoBehaviour
{
   

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => { ExitGame(); });
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(() => { ExitGame(); });
    }
    
    void ExitGame()
    {
        Application.Quit();
    }
}