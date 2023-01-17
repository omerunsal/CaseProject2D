using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeBase : MonoBehaviour
{
    public float G { get; set; }
    public float H { get; set; }
    public float F => G + H;
    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;
    public bool Walkable { get; set; }
    public ICoords Coords;
    public float GetDistance(NodeBase other) => Coords.GetDistance(other.Coords);


    public NodeBase Connection { get; private set; }
    public void SetConnection(NodeBase nodeBase) => Connection = nodeBase;
    [SerializeField] protected SpriteRenderer _renderer;


    public List<NodeBase> Neighbors { get; protected set; }
    public abstract void CacheNeighbors();

    public virtual void Init(bool walkable, ICoords coords)
    {
        Walkable = walkable;

        Coords = coords;
        transform.position = Coords.Pos;
    }
}


public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}