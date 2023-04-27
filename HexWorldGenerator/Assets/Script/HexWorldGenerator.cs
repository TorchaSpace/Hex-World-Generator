using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexWorldGenerator : MonoBehaviour
{
    //Constants
    private const float Hex_Radius = 2f;
    private const float Hex_Height = Hex_Radius * 2;

    //public variables
    public int width = 10;
    public int height = 10;
    public Material hexMaterial;

    //private variables
    private Vector3[,] hexPositions;

    private void Start()
    {
        hexPositions = new Vector3[width, height];

        float xOffset = Hex_Radius * 1.55f;
        float yOffset = Hex_Radius * 1.8f;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                float xPos = x * xOffset;
                float yPos = y * yOffset;

                if(x % 2 == 1)
                {
                    yPos += yOffset * 0.5f;
                }

                Vector3 hexPos = new Vector3(xPos, 0, yPos);
                hexPositions[x, y] = hexPos;

                GameObject hexGO = new GameObject("Hexagon");
                hexGO.transform.position = hexPos;
                hexGO.transform.SetParent(transform);

                MeshRenderer meshRenderer = hexGO.AddComponent<MeshRenderer>();
                meshRenderer.material = hexMaterial;

                MeshFilter meshFilter = hexGO.AddComponent<MeshFilter>();
                meshFilter.mesh = GenerateHexMesh();

                MeshCollider meshCollider = hexGO.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = meshFilter.mesh;
            }
        }
    }

    private Mesh GenerateHexMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] verticies = new Vector3[7];
        int[] triangles = new int[18];

        verticies[0] = new Vector3(0, 0, 0);
        for(int i = 1; i < 7; i++)
        {
            float angle = 60 * (i - 1) * Mathf.Deg2Rad;
            verticies[i] = new Vector3(Mathf.Cos(angle) * Hex_Radius, 0, Mathf.Sin(angle) * Hex_Radius);
        }

        triangles[0] = 0;
        triangles[1] = 6;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 6;
        triangles[5] = 2;
        triangles[6] = 2;
        triangles[7] = 6;
        triangles[8] = 3;
        triangles[9] = 3;
        triangles[10] = 6;
        triangles[11] = 4;
        triangles[12] = 4;
        triangles[13] = 6;
        triangles[14] = 5;
        triangles[15] = 5;
        triangles[16] = 6;
        triangles[17] = 1;

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
