using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance;

    public LayerMask wallLayer;
    public Vector2 gridOrigin;
    public int gridWidth = 100;
    public int gridHeight = 100;
    public float gridSize = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    public Queue<Vector2> FindPath(Vector2 startWorldPos, Vector2 targetWorldPos)
    {
        Vector2Int start = WorldToGrid(startWorldPos);
        Vector2Int target = WorldToGrid(targetWorldPos);

        Dictionary<Vector2Int, Node> nodes = new();
        List<Node> openSet = new();
        HashSet<Vector2Int> closedSet = new();

        Node startNode = new(start, IsWalkable(start));
        nodes[start] = startNode;
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node current = GetLowestFCostNode(openSet);

            if (current.Position == target)
            {
                return ReconstructPath(current);
            }

            openSet.Remove(current);
            closedSet.Add(current.Position);

            foreach (Vector2Int dir in Directions)
            {
                Vector2Int neighborPos = current.Position + dir;

                if (closedSet.Contains(neighborPos))
                    continue;

                bool walkable = IsWalkable(neighborPos);
                if (!walkable)
                    continue;

                if (!nodes.TryGetValue(neighborPos, out Node neighbor))
                {
                    neighbor = new Node(neighborPos, true);
                    nodes[neighborPos] = neighbor;
                }

                int tentativeG = current.GCost + 1;
                if (tentativeG < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = tentativeG;
                    neighbor.HCost = GetManhattanDistance(neighborPos, target);
                    neighbor.Parent = current;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    private Queue<Vector2> ReconstructPath(Node endNode)
    {
        List<Vector2> path = new();
        Node current = endNode;
        while (current != null)
        {
            path.Add(GridToWorld(current.Position));
            current = current.Parent;
        }
        path.Reverse();
        return new Queue<Vector2>(path);
    }

    private Node GetLowestFCostNode(List<Node> nodes)
    {
        Node best = nodes[0];
        foreach (Node node in nodes)
        {
            if (node.FCost < best.FCost || (node.FCost == best.FCost && node.HCost < best.HCost))
                best = node;
        }
        return best;
    }

    private bool IsWalkable(Vector2Int gridPos)
    {
        Vector2 worldPos = GridToWorld(gridPos);
        return Physics2D.OverlapCircle(worldPos, 0.2f, wallLayer) == null;
    }

    private int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private Vector2Int WorldToGrid(Vector2 worldPos)
    {
        Vector2 relative = worldPos - gridOrigin;
        return new Vector2Int(Mathf.RoundToInt(relative.x / gridSize), Mathf.RoundToInt(relative.y / gridSize));
    }

    private Vector2 GridToWorld(Vector2Int gridPos)
    {
        return gridOrigin + new Vector2(gridPos.x * gridSize, gridPos.y * gridSize);
    }

    private static readonly List<Vector2Int> Directions = new()
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
}