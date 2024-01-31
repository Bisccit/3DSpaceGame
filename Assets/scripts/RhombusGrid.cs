using System.Collections.Generic;
using UnityEngine;

public class RhombusGrid : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private int gridSize;
    [SerializeField] private float rhombusScale = 1;

    [SerializeField] private Color selectedColor, neighbourColor, defaultColor;

    private MatrixGrid6D grid;

    private void Start()
    {
        RhombicDodecahedron.mat = mat;

        grid = new MatrixGrid6D(gridSize);

        showRs();
    }

    private void showRs()
    {
        foreach (var rhombicd in grid)
        {
            rhombicd.Instantiate();
        }
        foreach (var rhombicd in grid)
        {
            rhombicd.obj.SetActive(false);
        }

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    grid.get(new Vector6(x, z, y, 0, 0, 0)).obj.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        RhombicDodecahedron[] buffer = null;

        foreach (var rhombicd in grid)
        {
            rhombicd.obj.SetActive(false);
        }

        grid.current.obj.SetActive(true);
        grid.current.setColor(selectedColor);

        //buffer = grid.getNeighbours(grid.current.position / rhombusScale);

        //if (buffer != null)
        //{
        //    foreach (var neighbour in buffer)
        //    {
        //        neighbour.obj.SetActive(false);
        //        neighbour.setColor(neighbourColor);
        //    }
            
        //}


        float direction = Input.GetKey(KeyCode.LeftShift) ? -1 : 1;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            grid.translate(new Vector6(0, 0, direction, 0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            grid.translate(new Vector6(direction, 0, 0, 0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            grid.translate(new Vector6(0, 0, 0, direction, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            grid.translate(new Vector6(0, 0, 0, 0, direction, 0));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            grid.translate(new Vector6(0, direction, 0, 0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            grid.translate(new Vector6(0, 0, 0, 0, 0, direction));
        }
    }

    private void OnDrawGizmos()
    {
        if (false && Application.isPlaying)
        {
            // for now draw only one rhombus
            List<Line> whiteLines = new List<Line>();

            // this is terrible

            foreach (var rhombicd in grid)
            {
                foreach (var rhombus in rhombicd.rhombi)
                {
                    for (int i = 0; i < rhombus.vertices.Length; i++)
                    {
                        Line line = new Line(rhombus.vertices[i], rhombus.vertices[(i + 1) % rhombus.vertices.Length]);

                        if (rhombus.center.Equals(MouseLook.camerapoint))
                        {
                            whiteLines.Add(line);
                        }
                    }
                }
            }

            foreach (var rhombicd in grid)
            {
                foreach (var rhombus in rhombicd.rhombi)
                {
                    for (int i = 0; i < rhombus.vertices.Length; i++)
                    {
                        Line line = new Line(rhombus.vertices[i], rhombus.vertices[(i + 1) % rhombus.vertices.Length]);

                        if (whiteLines.Contains(line))
                        {
                            Gizmos.color = Color.white;
                        }
                        Gizmos.DrawLine(line.start, line.end);
                        Gizmos.color = Color.gray;
                    }
                }
            }
        }
    }
}
