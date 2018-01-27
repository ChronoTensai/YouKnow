using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PathFinder3D
{
    public class AStar
    {
        private double omega;
        private PriorityQueue Open;
        private List<IVertex> Close;
        private double currentPathLength = 0;
        private IGraph graph;
        private IVertex start;
        private IVertex goal;
        bool firstRun = true;

        public AStar(double omega, IGraph graph, IVertex start, IVertex goal)
        {
            if (omega > 1 || omega < 0)
                throw new HeuristicRangeException();
            this.omega = omega;
            this.graph = graph;
            this.start = start;
            this.goal = goal;
        }

        public IEnumerator GetWay(RTWayInCubes ReturnWay)
        {
            Open = new PriorityQueue();
            Close = new List<IVertex>();
            var s = new List<IVertex>();
            s.Add(start);
            Open.Add(FFunction(s), s);
            int cyclesCount = 0;
            while (Open.Count != 0)
            {
                // choosing the optimum path out of queue
                var p = Open.First();
                Open.RemoveFirst();
                //consideration of its last node
                var x = p.Last();
                //in case this node has been visited the path is rejected
                if (Close.Contains(x))
                    continue;
                //The path cost is saved and the path is returned if the node is the target point.
                if (x.Equals(goal))
                {
                    currentPathLength = 0;
                    for (int i = 1; i < p.Count; i++)
                        currentPathLength += graph.Cost(p[i], p[i - 1]);
                    firstRun = false;
                    ReturnWay(p);
                    break;
                }
                //Current node is marked as visited
                Close.Add(x);
                // Disclosure of unvisited adjacent nodes
                foreach (var y in graph.Adjacent(x))
                    if (!Close.Contains(y))
                    {
                        var newPath = new List<IVertex>(p);
                        newPath.Add(y);
                        //During the first launch the algorithm works as a standard A* algorithm
                        //Otherwise, reject all potential paths if admissible function estimates their cost higher than the cost of the last found path
                        if (firstRun || FAccessibleFunction(newPath) < currentPathLength)
                            Open.Add(FFunction(newPath), newPath);
                    }
                if (cyclesCount == 5)
                {
                    cyclesCount = 0;
                    yield return null;
                }
                else
                    cyclesCount++;
            }
        }

        //Unacceptable function of the path cost estimation
        public double FFunction(List<IVertex> path)
        {
            double g = 0;
            for (int i = 1; i < path.Count; i++)
                g += graph.Cost(path[i - 1], path[i]);
            return (1 - omega) * g + omega * path.Last().HFunction();
        }

        //Accessory admissible function of path cost estimation 
        public double FAccessibleFunction(List<IVertex> path)
        {
            double g = 0;
            for (int i = 1; i < path.Count; i++)
                g += graph.Cost(path[i], path[i - 1]);
            return g + path.Last().HFunction();
        }
    }
    class HeuristicRangeException : Exception
    {
    }
}