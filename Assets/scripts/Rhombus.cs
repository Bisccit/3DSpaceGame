using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhombus
{
    public Vector3[] vertices { get;  }
    private float rhombusRatio = Mathf.Sqrt(0.5f);

    public Vector6 center { get; }

    public Rhombus(Vector6 center, Vector3 position, Vector2 scale, Vector3 rotation)
    {
        this.center = center;
        vertices = new Vector3[4];

        vertices[0] = position + new Vector3(-1 * scale.x, 0, 0);
        vertices[1] = position + new Vector3(0, -rhombusRatio * scale.y, 0);
        vertices[2] = position + new Vector3(1 * scale.x, 0, 0);
        vertices[3] = position + new Vector3(0, rhombusRatio * scale.y, 0);

        // rotate the damn thing
        for (int i = 0; i < 4; i++)
        {
            vertices[i] -= position;
            vertices[i] = Quaternion.Euler(rotation) * vertices[i];
            vertices[i] += position;
        }
    }
}
