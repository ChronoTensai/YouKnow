using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PathFinder3D
{
    public interface IGraph
    {
        //List of adjacent nodes
        List<IVertex> Adjacent(IVertex vertex);
        //Cost of the path between adjacent nodes
        double Cost(IVertex a, IVertex b);
    }
}