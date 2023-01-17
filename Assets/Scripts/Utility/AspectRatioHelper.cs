using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioHelper : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private ScriptableSquareGrid _scriptableGrid;

    void Start()
    {
        mainCamera = Camera.main;

        FitCamera();
    }

    private void FitCamera()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = _scriptableGrid._gridWidth / _scriptableGrid._gridHeight;

        if (screenRatio >= targetRatio)
        {
            mainCamera.orthographicSize = _scriptableGrid._gridHeight / 2;
        }
        else
        {
            float diffSize = targetRatio / screenRatio;
            mainCamera.orthographicSize = _scriptableGrid._gridHeight / 2 * diffSize;
        }

        mainCamera.transform.position = new Vector3((float)_scriptableGrid._gridWidth / 2 -.5f,
            (float)_scriptableGrid._gridHeight / 2-.5f, -10);
    }
}