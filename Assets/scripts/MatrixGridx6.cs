using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGrid6D : IEnumerable<RhombicDodecahedron>
{
    private RhombicDodecahedron[,,] grid;
    private int size;

    public RhombicDodecahedron current = null;

    public MatrixGrid6D(int size)
    {
        this.size = size;
        grid = new RhombicDodecahedron[size, size, size];

        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                for (int y = 0; y < size; y++)
                {
                    grid[x, z, y] = new RhombicDodecahedron(new Vector6(x,z,y,0,0,0), 1, Vector6.zero);
                }
            }
        }

        current = grid[0, 0, 0];
    }

    public void translate(Vector6 direction)
    {
        current = get(current.position + direction);
    }

    public void put(RhombicDodecahedron rhombicDodecahedron, Vector6 position)
    {
        Vector3 gridPosition = position.toGridVector();

        grid[(int)gridPosition.x, (int)gridPosition.z, (int)gridPosition.y] = rhombicDodecahedron;
    }

    public RhombicDodecahedron get(Vector6 position)
    {
        Vector3 gridPosition = position.toGridVector();

        return grid[
            (int)Mathf.Max(Mathf.Min(gridPosition.x, size - 1), 0),
            (int)Mathf.Max(Mathf.Min(gridPosition.z, size - 1), 0),
            (int)Mathf.Max(Mathf.Min(gridPosition.y, size - 1), 0)];
    }

    public RhombicDodecahedron[] getNeighbours(Vector6 position)
    {
        List<RhombicDodecahedron> res = new();

        for (int factor = -1; factor <= 1; factor += 2)
        {
            for (int i = 0; i < 6; i++)
            {
                int[] mask = new int[6];
                mask[i] = 1;
                Vector6 offset = new Vector6(mask[0], mask[1], mask[2], mask[3], mask[4], mask[5]);
                res.Add(get(position - offset * factor));
            }
        }

        //res.Add(get(position + new Vector6(1,0,0,0,0,0)));
        //res.Add(get(position + new Vector6(0, 1, 0, 0, 0, 0)));
        //res.Add(get(position + new Vector6(0, 0, 1, 0, 0, 0)));
        //res.Add(get(position + new Vector6(0, 0, 0, 0, 0, 1)));
        //res.Add(get(position + new Vector6(0, 0, 0, 0, 1, 0)));
        //res.Add(get(position + new Vector6(0, 0, 0, 1, 0, 0)));

        return res.ToArray();
    }

    public IEnumerator<RhombicDodecahedron> GetEnumerator()
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                for (int y = 0; y < size; y++)
                {
                    yield return grid[x, z, y];  
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
