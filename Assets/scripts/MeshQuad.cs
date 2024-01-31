using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshQuad
{
    protected List<int> vertexIndices;
    private bool flipped;

    /// <summary>
    /// Create a mesh quad containing the index info of 4 vertices
    /// </summary>
    /// <param name="a">bottom left</param>
    /// <param name="b">bottom right</param>
    /// <param name="c">top left</param>
    /// <param name="d">top right</param>
    /// <param name="flipped">should the face be flipped?</param>
    public MeshQuad(int a, int b, int c, int d, bool flipped = false)
    {
        vertexIndices = new List<int>() { a, b, c, d };
        this.flipped = flipped;
    }

    public int assignTriangles(int[] triangles, int i)
    {
        if (flipped)
        {
            triangles[i] = vertexIndices[0];
            triangles[i + 1] = vertexIndices[2];
            triangles[i + 2] = vertexIndices[1];
            triangles[i + 3] = vertexIndices[1];
            triangles[i + 4] = vertexIndices[2];
            triangles[i + 5] = vertexIndices[3];
        }
        else
        {
            triangles[i] = vertexIndices[1];
            triangles[i + 1] = vertexIndices[3];
            triangles[i + 2] = vertexIndices[0];
            triangles[i + 3] = vertexIndices[0];
            triangles[i + 4] = vertexIndices[3];
            triangles[i + 5] = vertexIndices[2];
        }

        return i + 6;
    }
}
