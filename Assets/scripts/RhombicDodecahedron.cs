using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class RhombicDodecahedron
{
    public static Material mat;

    public List<Rhombus> rhombi = new();

    public Vector6 position;

    public GameObject obj;

    public RhombicDodecahedron(Vector6 position, float scale, Vector6 rotation)
    {
        Vector3 center = position.toVector3();
        this.position = position;

        // x
        rhombi.Add(new Rhombus(position, center + Vector6.normalX * scale, new Vector2(1, 1), new Vector3(0, 90, 0)));
        rhombi.Add(new Rhombus(position, center - Vector6.normalX * scale, new Vector2(1, 1), new Vector3(0, -90, 0)));

        // z
        rhombi.Add(new Rhombus(position, center + Vector6.normalZ * scale, new Vector2(1, 1), new Vector3(0, 0, 0)));
        rhombi.Add(new Rhombus(position, center - Vector6.normalZ * scale, new Vector2(1, 1), new Vector3(0, 0, 0)));

        // t
        rhombi.Add(new Rhombus(position, center + Vector6.normalY * scale, new Vector2(1, 1), new Vector3(45, 45, 90)));
        rhombi.Add(new Rhombus(position, center - Vector6.normalY * scale, new Vector2(1, 1), new Vector3(45, 45, -90)));

        // u
        rhombi.Add(new Rhombus(position, center + Vector6.normalT * scale, new Vector2(1, 1), new Vector3(-45, -45, 90)));
        rhombi.Add(new Rhombus(position, center - Vector6.normalT * scale, new Vector2(1, 1), new Vector3(-45, -45, -90)));

        // v
        rhombi.Add(new Rhombus(position, center + Vector6.normalU * scale, new Vector2(1, 1), new Vector3(-45, 45, -90)));
        rhombi.Add(new Rhombus(position, center - Vector6.normalU * scale, new Vector2(1, 1), new Vector3(-45, 45, 90)));

        // w
        rhombi.Add(new Rhombus(position, center + Vector6.normalV * scale, new Vector2(1, 1), new Vector3(45, -45, -90)));
        rhombi.Add(new Rhombus(position, center - Vector6.normalV * scale, new Vector2(1, 1), new Vector3(45, -45, 90)));
    }

    public void Instantiate()
    {
        // maybe use LineRenderer?

        Mesh mesh = MeshGenerator.generateRhombicDodecahedron(rhombi);

        obj = new GameObject();
        //rhombusObj.transform.position = rhombi[i].center.toVector3();
        obj.transform.position = position.toVector3();
        obj.name = "RhombicDedocahedron";

        obj.AddComponent(typeof(MeshFilter));
        obj.AddComponent(typeof(MeshRenderer));

        obj.GetComponent<MeshFilter>().mesh = mesh;
        obj.GetComponent<MeshRenderer>().material = mat;
    }

    public void setOpacity(float opacity)
    {
        Color color = obj.transform.GetComponentInChildren<Renderer>().material.color;
        setColor(new Color(color.r, color.g, color.b, opacity));
    }

    public void setColor(Color color)
    {
        obj.transform.GetComponent<Renderer>().material.color = color;
    }
}
