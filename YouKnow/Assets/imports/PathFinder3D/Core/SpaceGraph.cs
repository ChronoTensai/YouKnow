using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PathFinder3D
{
    class SpaceGraph : IGraph
    {
        SpaceConstraints constraints;
        float sideLength;
        int xCubes, yCubes, zCubes;
        IVertex start, goal;
        double omega;
        MonoBehaviour mb;
        RTWayInCoordinates ReturnCoordinates;

        public List<IVertex> Adjacent(IVertex vertex)
        {
            
            int x = ((SpaceCube)vertex).x;
            int y = ((SpaceCube)vertex).y;
            int z = ((SpaceCube)vertex).z;
            List<IVertex> adj = new List<IVertex>();
			for (int i = -1; i <= 1; i++)
				for (int j = -1; j <= 1; j++)
					for (int k = -1; k <= 1; k++)
						try
						{
                            if (!IsIndexInRange(x + i, y + j, z + k)) throw new IndexOutOfRangeException();
                            Vector3 thisCube = new Vector3();
                            thisCube.x = (float)(x * sideLength + sideLength / 2 + constraints.xMin);
                            thisCube.y = (float)(y * sideLength + sideLength / 2 + constraints.yMin);
                            thisCube.z = (float)(z * sideLength + sideLength / 2 + constraints.zMin);
                            Vector3 tryingCube = new Vector3();
                            tryingCube.x = (float)((x + i) * sideLength + sideLength / 2 + constraints.xMin);
                            tryingCube.y = (float)((y + j) * sideLength + sideLength / 2 + constraints.yMin);
                            tryingCube.z = (float)((z + k) * sideLength + sideLength / 2 + constraints.zMin);
                            Vector3 direction = tryingCube - thisCube;
                            if (!Physics.BoxCast(thisCube,
                                new Vector3(sideLength / 2, sideLength / 2, sideLength / 2),
                                direction,
                                Quaternion.identity,
                                direction.magnitude))
                                adj.Add((IVertex)new SpaceCube(x + i, y + j, z + k, sideLength, (SpaceCube)goal));
						}
						catch (IndexOutOfRangeException)
						{
						}

            return adj;
        }
        bool IsIndexInRange(int x, int y, int z)
        {
            return (x * sideLength + sideLength / 2 + constraints.xMin <= constraints.xMax) &&
                (y * sideLength + sideLength / 2 + constraints.yMin <= constraints.yMax) &&
                (z * sideLength + sideLength / 2 + constraints.zMin <= constraints.zMax) && x>=0 && y>=0 && z>=0;
        }

        public double Cost(IVertex ai, IVertex bi)
        {
            SpaceCube a = (SpaceCube)ai;
            SpaceCube b = (SpaceCube)bi;
            // Cubes must be ajacent!!
            int dist = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z); // Distance from a to b
            return sideLength * Math.Sqrt(dist);

        }

        public SpaceGraph(SpaceConstraints constraints, float sideLength, double omega, MonoBehaviour mb)
        {
            this.omega = omega;
            this.sideLength = sideLength;
            this.constraints = constraints;
            this.mb = mb;
            xCubes = Convert.ToInt32(Math.Truncate((constraints.xMax - constraints.xMin) / sideLength));
            yCubes = Convert.ToInt32(Math.Truncate((constraints.yMax - constraints.yMin) / sideLength));
            zCubes = Convert.ToInt32(Math.Truncate((constraints.zMax - constraints.zMin) / sideLength));
        }

        public void SetStart(Vector3 start)
        {
            if (goal == null)
                throw new GoalIsNotSetException();
            if (start.x < constraints.xMin || start.x > constraints.xMin + xCubes * sideLength ||
                start.y < constraints.yMin || start.y > constraints.yMin + yCubes * sideLength ||
                start.z < constraints.zMin || start.z > constraints.zMin + zCubes * sideLength)
                throw new StartOutOfAreaException();
            int x = Convert.ToInt32(Math.Truncate((start.x - constraints.xMin) / sideLength));
            int y = Convert.ToInt32(Math.Truncate((start.y - constraints.yMin) / sideLength));
            int z = Convert.ToInt32(Math.Truncate((start.z - constraints.zMin) / sideLength));
			this.start = new SpaceCube(x, y, z, sideLength, (SpaceCube)goal);
        }

        public void SetGoal(Vector3 goal)
        {
            if (goal.x < constraints.xMin || goal.x > constraints.xMin + xCubes * sideLength ||
                goal.y < constraints.yMin || goal.y > constraints.yMin + yCubes * sideLength ||
                goal.z < constraints.zMin || goal.z > constraints.zMin + zCubes * sideLength)
                throw new GoalOutOfAreaException();
            int x = Convert.ToInt32(Math.Truncate((goal.x - constraints.xMin) / sideLength));
            int y = Convert.ToInt32(Math.Truncate((goal.y - constraints.yMin) / sideLength));
            int z = Convert.ToInt32(Math.Truncate((goal.z - constraints.zMin) / sideLength));
            this.goal = new SpaceCube(x, y, z, sideLength, null);
        }

        
        public Coroutine GetWay(RTWayInCoordinates ReturnCoordinates)
        {
            if (goal == null || start == null)
                throw new GoalIsNotSetException();
            AStar finder = new AStar(omega, this, start, goal);
            this.ReturnCoordinates = ReturnCoordinates;
            return mb.StartCoroutine(finder.GetWay(ReturnWay));
        }
        public void ReturnWay(List<IVertex> wayInCubes)
        {
            List<Vector3> way = new List<Vector3>();
            foreach (SpaceCube cube in wayInCubes)
            {
                // Calculation of cube center coordinate by its number, reduction to Vector3
                Vector3 vCube = new Vector3();
                vCube.x = (float)(cube.x * sideLength + sideLength / 2 + constraints.xMin);
                vCube.y = (float)(cube.y * sideLength + sideLength / 2 + constraints.yMin);
                vCube.z = (float)(cube.z * sideLength + sideLength / 2 + constraints.zMin);
                way.Add(vCube);
            }
            ReturnCoordinates(way);
        }

    }

    public struct SpaceConstraints
    {
        public float xMin, xMax, yMin, yMax, zMin, zMax;

        public SpaceConstraints(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
            this.zMin = zMin;
            this.zMax = zMax;
        }
    }


    public class GoalIsNotSetException : Exception
    {
    }
    public class StartOutOfAreaException : Exception
    {
    }
    public class GoalOutOfAreaException : Exception
    {
    }

    public delegate void RTWayInCubes(List<IVertex> way);
    public delegate void RTWayInCoordinates(List<Vector3> way);
}
