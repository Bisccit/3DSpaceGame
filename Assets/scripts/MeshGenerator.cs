using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeshGenerator
{

    public static Mesh generateRhombicDodecahedron(List<Rhombus> faces)
    {
        List<MeshQuad> meshQuads = new();
        List<Vector3> vertices = new();

        for (int i = 0; i < faces.Count; i++)
        {
            Vector3[] corners = new Vector3[4] { faces[i].vertices[0], faces[i].vertices[1], faces[i].vertices[3], faces[i].vertices[2] };

            vertices.Add(corners[0]);
            vertices.Add(corners[1]);
            vertices.Add(corners[2]);
            vertices.Add(corners[3]);

            vertices.Add(corners[0]);
            vertices.Add(corners[1]);
            vertices.Add(corners[2]);
            vertices.Add(corners[3]);

            meshQuads.Add(new MeshQuad(vertices.Count - 8, vertices.Count - 7, vertices.Count - 6, vertices.Count - 5));
            meshQuads.Add(new MeshQuad(vertices.Count - 4, vertices.Count - 3, vertices.Count - 2, vertices.Count - 1, true));
        }

        return generateMesh(meshQuads, vertices);
    }

    private static Mesh generateMesh(List<MeshQuad> meshQuads, List<Vector3> vertices)
    {
        int[] triangles = new int[meshQuads.Count * 6];

        for (int i = 0, tris = 0; i < meshQuads.Count; i++)
        {
            tris = meshQuads[i].assignTriangles(triangles, tris);
        }

        Mesh mesh = new Mesh();

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }
}
