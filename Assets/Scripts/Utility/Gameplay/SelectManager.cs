using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Unit currentSelectedObject;
    public GameObject cacheSelectedObject;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectedObject = null;
    }

    private bool isUI = false;

    // Update is called once per frame
    void Update()
    {
        if (InputHelper.LeftClickDown)
        {
            if (UtilClass.GetComponentFromRay(ref isUI) != null)
            {
                currentSelectedObject = UtilClass.GetComponentFromRay(ref isUI).GetComponent<Unit>();

                bool isBuilding = currentSelectedObject.TryGetComponent(out IBuilding iBuilding);
                if (isBuilding)
                {
                    GameEvents.ShowInfoPanel?.Invoke(true);
                    GameEvents.SetInfoPanel?.Invoke(currentSelectedObject as Building);
                }
                else
                {
                    GameEvents.ShowInfoPanel?.Invoke(false);
                }
            }
            else
            {
                currentSelectedObject = null;

                if (!isUI)
                {
                    GameEvents.ShowInfoPanel?.Invoke(false);
                }
            }

            if (UtilClass.GetComponentFromRay(ref isUI) != null)
            {
                cacheSelectedObject = UtilClass.GetComponentFromRay(ref isUI);
            }
        }
    }
}