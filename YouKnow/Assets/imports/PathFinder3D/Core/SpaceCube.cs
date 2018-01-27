using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PathFinder3D
{
    class SpaceCube : IVertex
    {
        public int x, y, z;
        double sideLength;
        SpaceCube goal;

        public double HFunction()
        {
            if (goal == null)
                return 0;
            return Math.Sqrt(Math.Pow(sideLength * (x - goal.x), 2) + Math.Pow(sideLength * (y - goal.y), 2) + Math.Pow(sideLength * (z - goal.z), 2));
        }

        public SpaceCube(int x, int y, int z, double sideLength, SpaceCube goal)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.sideLength = sideLength;
            this.goal = goal;
        }

        public override bool Equals(object otherAbstr)
        {
            SpaceCube other = (SpaceCube)otherAbstr;
            return other.x == x && other.y == y && other.z == z;
        }
    }
}
