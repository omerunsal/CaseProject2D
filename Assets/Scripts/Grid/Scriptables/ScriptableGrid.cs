using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableGrid : ScriptableObject {
    [SerializeField] protected NodeBase nodeBasePrefab;
    [Range(0,6)] private int _obstacleWeight = 0;
    public abstract Dictionary<Vector2, NodeBase> GenerateGrid();
        
    protected bool DecideIfObstacle() => Random.Range(1, 20) > _obstacleWeight;
}
