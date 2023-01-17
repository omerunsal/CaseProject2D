using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] public ScriptableGrid _scriptableGrid;
    
    void Awake() => Instance = this;

    public Dictionary<Vector2, NodeBase> Tiles { get; private set; }


    private void Start()
    {
        Tiles = _scriptableGrid.GenerateGrid();

        foreach (var tile in Tiles.Values) tile.CacheNeighbors();
    }

    public NodeBase GetTileAtPosition(Vector2 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;
}