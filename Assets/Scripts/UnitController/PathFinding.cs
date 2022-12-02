using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт нахождения пути при перемещении
public class PathFinding 
{

    private int Stright_cost = 10;
    private int Diagonal_cost = 14;

    public static PathFinding Instance { get; private set; }
    public Map<PathNode> grid { get; private set; }

    private List<PathNode> OpenNodes;
    private List<PathNode> ClosedNodes;
    private List<PathNode> pathNodes;

    public PathFinding(int W, int H)
    {
        Instance = this;
        grid = new Map<PathNode>(W, H, 1, new Vector3(0,0,0), (Map<PathNode>g, int w, int h) => new PathNode(grid, w, h));
    }
    public List<Vector3> FindPath(Vector3 start, Vector3 fin)
    {

        PathNode startNode = grid.GetValue(start);
        PathNode finNode = grid.GetValue(fin);
        List<PathNode> tray = FindPath(startNode, finNode);
        List<Vector3> res = new List<Vector3>();
        if (tray !=  null)
        {
            for (int i = 0; i < tray.Count; i++)
            {
                res.Add(new Vector3(tray[i].x + grid.CellSize * 0.5F, tray[i].y + grid.CellSize * 0.5F, 0)  );
            }
            return res;
        }
        return null;

    }
    public List<PathNode> FindPath(PathNode start, PathNode fin)
    {
        PathNode startNode = start;
        PathNode finalNode = fin;

        OpenNodes = new List<PathNode> { startNode};
        ClosedNodes = new List<PathNode>();

        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                PathNode pathNode = grid.GetValue(new Vector3(i, j, 0));
                pathNode.gCost = int.MaxValue;
                pathNode.CalcFcost();
                pathNode.WhereCameFrom = null;

            }
        }

        startNode.gCost = 0;
        startNode.hCost = DistanceToFin(startNode, finalNode);
        startNode.CalcFcost();

        while(OpenNodes.Count > 0){
            PathNode cur = LowestFcost();
            if(cur == finalNode)
            {
                return CalculatePath(finalNode);
            }
            OpenNodes.Remove(cur);
            ClosedNodes.Add(cur);

            foreach (var neigh in OpenNeighbours(cur))
            {
                if (ClosedNodes.Contains(neigh))
                    continue;
                if (!neigh.is_walkable)
                {
                    ClosedNodes.Add(neigh);
                    continue;
                }

                int CostToNeigh = cur.gCost + DistanceToFin(cur, neigh);
                if(CostToNeigh < neigh.gCost)
                {
                    neigh.WhereCameFrom = cur;
                    neigh.gCost = CostToNeigh;
                    neigh.hCost = DistanceToFin(neigh, finalNode);
                    neigh.CalcFcost();

                    if (!OpenNodes.Contains(neigh))
                        OpenNodes.Add(neigh);
                }
            }
        }

        return null;
    }

    private List<PathNode> OpenNeighbours(PathNode node)
    {
        List<PathNode> Neighs = new List<PathNode>();

        if(node.x - 1 > 0)
        {
            Neighs.Add(grid.GetValue(new Vector3(node.x - 1, node.y)));
            if (node.y - 1 > 0)
            {
                Neighs.Add(grid.GetValue(new Vector3(node.x - 1, node.y - 1)));
            }
            if (node.y + 1 < grid.height)
            {
                Neighs.Add(grid.GetValue(new Vector3(node.x - 1, node.y + 1)));
            }
        }
        if (node.x + 1 < grid.width)
        {
            Neighs.Add(grid.GetValue(new Vector3(node.x + 1, node.y)));
            if (node.y - 1 > 0)
            {
                Neighs.Add(grid.GetValue(new Vector3(node.x + 1, node.y - 1)));
            }
            if (node.y + 1 < grid.height)
            {
                Neighs.Add(grid.GetValue(new Vector3(node.x + 1, node.y + 1)));
            }
        }
        if (node.y - 1 > 0)
        {
            Neighs.Add(grid.GetValue(new Vector3(node.x, node.y - 1)));
        }
        if (node.y + 1 < grid.height)
        {
            Neighs.Add(grid.GetValue(new Vector3(node.x, node.y + 1)));
        }

        return Neighs;
    }

    private List<PathNode> CalculatePath(PathNode fin)
    {
        List<PathNode> Tray = new List<PathNode>();
        Tray.Add(fin);
        PathNode node = fin.WhereCameFrom;
        while (node != null)
        {
            Tray.Add(node);
            node = node.WhereCameFrom;
        }
        Tray.Reverse();
        return Tray;
    }
    private int DistanceToFin(PathNode start, PathNode fin)
    {
        int xDist = Mathf.Abs(start.x - fin.x);
        int yDist = Mathf.Abs(start.y - fin.y);
        return Diagonal_cost * Mathf.Min(xDist, yDist) + Stright_cost * Mathf.Abs(xDist - yDist);
    }

    private PathNode LowestFcost()
    {
        PathNode lowest = OpenNodes[0];
        for (int i = 0; i < OpenNodes.Count; i++)
        {
            if(lowest.fCost > OpenNodes[i].fCost)
            {
                lowest = OpenNodes[i];
            }
        }
        return lowest;
    }
}
