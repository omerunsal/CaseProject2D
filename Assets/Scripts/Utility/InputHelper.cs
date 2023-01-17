using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHelper
{
    public static bool LeftClickDown => Input.GetMouseButtonDown(0);
    public static bool RightClickDown => Input.GetMouseButtonDown(1);
}