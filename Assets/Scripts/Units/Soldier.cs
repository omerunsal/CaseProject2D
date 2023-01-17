using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Soldier : Unit
{
    public ScriptableSoldierData SoldierData;

    private GridManager Grid;

    public bool isMoving;

    private void Start()
    {
        isMoving = false;
        Grid = FindObjectOfType<GridManager>();
    }

    public void Move(NodeBase targetNode)
    {
        NodeBase initialPosition = FindCurrentPositionNode();
        var path = Pathfinding.FindPath(initialPosition, targetNode);
        if (path != null)
        {
            path.Reverse();
            StartCoroutine(MovementHelper.Move(path, this.gameObject));
            initialPosition.Walkable = true;
            targetNode.Walkable = false;
        }
    }

    NodeBase FindCurrentPositionNode()
    {
        return Grid.Tiles.Where(x => x.Key == new Vector2(transform.position.x, transform.position.y)).First().Value;
    }

    Vector2 FindCurrentPosition()
    {
        return Grid.Tiles.Where(x => x.Key == new Vector2(transform.position.x, transform.position.y)).First().Key;
    }
}