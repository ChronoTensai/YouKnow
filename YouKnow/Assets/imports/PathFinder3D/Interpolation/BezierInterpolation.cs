using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.PathFinder3D
{
    //we don’t recommend this class for using because it’s alternative one and, frankly, is far from perfection
    public class BezierInterpolation
    {
        List<Vector3> way;
        int n;
        int segments_num;
        List<B_curve> curves;

        //Cubic Bezier curve definition
        struct B_curve
        {
            public Vector3 p0;
            public Vector3 p1;
            public Vector3 p2;
            public Vector3 p3;
            public float length;
        }

        //class constructor
        public BezierInterpolation(List<Vector3> inp_way,float side_length)
        {
            segments_num = (int)(side_length * 10F);
            way = inp_way;
        }

        //establishing a spline
        public void CalcCurves() {
            n = way.Count - 1;
            curves = new List<B_curve>(n);
            for (int i = 0; i < n; i++)
            {
                B_curve cur_curve;
                if (i == 0)
                {
                    cur_curve.p0 = way[i];
                    cur_curve.p1 = way[i] + (way[i + 1] - way[i]) * 0.5F;
                    cur_curve.p2 = cur_curve.p1;
                    cur_curve.p3 = way[i + 1];
                }else
                {
                    cur_curve.p0 = way[i];
                    cur_curve.p1 = cur_curve.p0 + (cur_curve.p0 - curves[curves.Count - 1].p2);
                    cur_curve.p2 = way[i] + (way[i + 1] - way[i]) * 0.5F;
                    cur_curve.p3 = way[i + 1];
                }
                cur_curve.length = gauss_curve_length_4_nodes(cur_curve.p0, cur_curve.p1, cur_curve.p2, cur_curve.p3, 0F, 1F);
                curves.Add(cur_curve);
            }
        }

        //creating a new path by spline points
        public List<Vector3> BuildSpline()
        {
            List<Vector3> spline_points = new List<Vector3>();

            float step = 1f / (float)segments_num;
            for (int i = 0; i < n; i++)
            {
               for (float t = 0; t < 1; t += step)
                {
                    spline_points.Add(B(curves[i].p0, curves[i].p1, curves[i].p2, curves[i].p3,t));
                }
            }
            return spline_points;
        }

        // integrand of a length formula of a parametric curve 
        float L(Vector3 p0,Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float dBxdt = -3 * Mathf.Pow(1F - t, 2) * p0.x + (3 * Mathf.Pow(1F - t, 2) - 6 * t + 6 * t * t) * p1.x + (6 * t - 9 * t * t) * p2.x + 3 * t * t * p3.x;
            float dBydt = -3 * Mathf.Pow(1F - t, 2) * p0.y + (3 * Mathf.Pow(1F - t, 2) - 6 * t + 6 * t * t) * p1.y + (6 * t - 9 * t * t) * p2.y + 3 * t * t * p3.y;
            float dBzdt = -3 * Mathf.Pow(1F - t, 2) * p0.z + (3 * Mathf.Pow(1F - t, 2) - 6 * t + 6 * t * t) * p1.z + (6 * t - 9 * t * t) * p2.z + 3 * t * t * p3.z;
            return Mathf.Sqrt(dBxdt*dBxdt+dBydt*dBydt+dBzdt*dBzdt);
        }

        //curve length calculation with four-point Gausse quadrature method
        float gauss_curve_length_4_nodes(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float a, float b)
        {
            float a0 = 0.3478548451F;
            float a1 = 0.6521451549F;
            float a2 = 0.6521451549F;
            float a3 = 0.3478548451F;
            float t0 = -0.8611363115F;
            float t1 = -0.3399810436F;
            float t2 = 0.3399810436F;
            float t3 = 0.8611363115F;
            float s0 = a0 * L(p0, p1, p2, p3, (a + b) / 2 + ((b - a) / 2) * t0);
            float s1 = a1 * L(p0, p1, p2, p3, (a + b) / 2 + ((b - a) / 2) * t1);
            float s2 = a2 * L(p0, p1, p2, p3, (a + b) / 2 + ((b - a) / 2) * t2);
            float s3 = a3 * L(p0, p1, p2, p3, (a + b) / 2 + ((b - a) / 2) * t3);
            return ((float)(b - a) / 2) * (float)(s0 + s1 + s2 + s3);
        }

        //Cubic Bezier curve equation
        public Vector3 B(Vector3 p0,Vector3 p1,Vector3 p2,Vector3 p3,float t) {
            return Mathf.Pow(1.0F-t,3)*p0 + 3*t* Mathf.Pow(1.0F - t, 2)*p1 + 3*t*t*(1.0F-t)*p2+t*t*t*p3;
        }

    }
}
