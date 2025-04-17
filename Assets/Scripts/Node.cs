using UnityEngine;

public class Node : MonoBehaviour
{
	public Vector2Int Position;
    public bool Walkable;

    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;

    public Node Parent;

    public Node(Vector2Int pos, bool walkable)
    {
        Position = pos;
        Walkable = walkable;
    }
}
