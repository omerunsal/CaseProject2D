using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder<T> : MonoBehaviour where T : Unit
{
    public T BuildProduction(T buildUnit, Vector3 buildPosition)
    {
        return Instantiate(buildUnit, buildPosition, Quaternion.identity);
    }
}