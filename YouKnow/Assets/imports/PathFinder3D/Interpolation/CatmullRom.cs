using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.PathFinder3D
{
    public class CatmullRom
    {
        List<Vector3> way;
        int segments_num;

        //class constructor
        public CatmullRom(List<Vector3> inp_way, float side_length) {
            //calculation number of joints for each spline curve
            segments_num = (int)(side_length * 10F);
            way = inp_way;
        }

        //polyline construction on spline curves
        public List<Vector3> GetSpline()
        {
            //accessory extreme points adding
            way.Insert(0,way[0]+(way[0]-way[1]));
            way.Insert(way.Count,way[way.Count-1]+(way[way.Count-1]-way[way.Count-2]));

            List<Vector3> spline_points = new List<Vector3>();
            float step = 1f / (float)segments_num;

            //spline points obtaining
            for (int i = 1; i <= way.Count - 3; i++)
            {
                for (float t = 0; t < 1; t += step)
                {
                    spline_points.Add(CatmullRomEq(way[i-1],way[i],way[i+1],way[i+2],t));
                }
            }
            
            return spline_points;
        }

        //Catmull-Rom spline equation
        public Vector3 CatmullRomEq(Vector3 P0,Vector3 P1,Vector3 P2,Vector3 P3,float t) {
            return 0.5f * (
                -t * (1 - t) * (1 - t) * P0
                + (2 - 5 * t * t + 3 * t * t * t) * P1
                + t * (1 + 4 * t - 3 * t * t) * P2
                - t * t * (1 - t) * P3
                );
        }
    }
}