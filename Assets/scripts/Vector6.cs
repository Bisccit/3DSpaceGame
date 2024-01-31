using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Coordinates used for the dodecahedronal grid
/// IMPORTANT
/// 
/// x = x
/// z = z
/// y = y
/// t = y - x       ==> 
/// u = y - z
/// v = y - x - z
/// </summary>
[Serializable]
public struct Vector6
{
    public float x, z, y, t, u, v;

    public static Vector3 normalX { get { return new Vector3(1, 0, 0); } }
    public static Vector3 normalZ { get { return new Vector3(0, 0, 1); } }
    public static Vector3 normalY { get { return new Vector3(-0.5f, Mathf.Sqrt(0.5f), -0.5f); } }
    public static Vector3 normalT { get { return new Vector3(-0.5f, Mathf.Sqrt(0.5f), 0.5f); } }
    public static Vector3 normalU { get { return new Vector3(0.5f, Mathf.Sqrt(0.5f), 0.5f); } }
    public static Vector3 normalV { get { return new Vector3(0.5f, Mathf.Sqrt(0.5f), -0.5f); } }

    public static Vector6 zero { get { return new Vector6(0,0,0,0,0,0); } }

    public static Vector6 one { get { return new Vector6(1, 1, 1, 1, 1, 1); } }

    public Vector6(float x, float z, float y, float t, float u, float v)
    {
        this.x = x;
        this.z = z;
        this.y = y;
        this.t = t;
        this.u = u;
        this.v = v;
    }

    public Vector3 toVector3()
    {
        Vector3 res = Vector3.zero;

        res += x * normalX;
        res += z * normalZ;
        res += y * normalY;
        res += t * normalT;
        res += u * normalU;
        res += v * normalV;

        return res;
    }

    public Vector3 toGridVector()
    {
        Vector3 res = new Vector3();

        res.x += x;
        res.y += y;
        res.z += z;

        res.x += t;
        res.y += t;

        res.z += u;
        res.y += u;

        res.x += v;
        res.y += v;
        res.z += v;

        return res;
    }

    public static Vector6 fromGridVector(Vector3 a)
    {
        return new Vector6(a.x,a.y,a.z,0,0,0);
    }

    public static Vector6 operator *(Vector6 a, int b)
    {
        return new Vector6(a.x * b, a.z * b, a.y * b, a.t * b, a.u * b, a.v * b);
    }

    public static Vector6 operator *(Vector6 a, float b)
    {
        return new Vector6(a.x * b, a.z * b, a.y * b, a.t * b, a.u * b, a.v * b);
    }

    public static Vector6 operator /(Vector6 a, float b)
    {
        return new Vector6(a.x / b, a.z / b, a.y / b, a.t / b, a.u / b, a.v / b);
    }

    public static Vector6 operator -(Vector6 a, Vector6 b)
    {
        return new Vector6(a.x - b.x, a.z - b.z, a.y - b.y, a.t - b.t, a.u - b.u, a.v - b.v);
    }

    public static Vector6 operator +(Vector6 a, Vector6 b)
    {
        return new Vector6(a.x + b.x, a.z + b.z, a.y + b.y, a.t + b.t, a.u + b.u, a.v + b.v);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector6)
        {
            Vector6 other = (Vector6)obj;

            return x == other.x && z == other.z && y == other.y && t == other.t && u == other.u && v == other.v;
        }

        return false;
    }

    public override string ToString()
    {
        StringBuilder res = new StringBuilder();

        res.Append('(').
            Append(x).Append(", ").
            Append(z).Append(", ").
            Append(y).Append(", ").
            Append(t).Append(", ").
            Append(u).Append(", ").
            Append(v).
            Append(')');

        return res.ToString();
    }
}

public class Line
{
    public Vector3 start { get; }
    public Vector3 end { get; }

    public Line(Vector3 a, Vector3 b)
    {
        start = a;
        end = b;
    }

    public override bool Equals(object obj)
    {
        if (obj is Line)
        {
            Line other = (Line)obj;

            return (start.Equals(other.start) && end.Equals(other.end)) || (start.Equals(other.end) && end.Equals(other.start));
        }
        return false;
    }
}
