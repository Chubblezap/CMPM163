using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
//[ExecuteInEditMode]
public class CreateMeshWater : MonoBehaviour
{
    int numCellsX = 50;
    int numCellsY = 50;

    int xSize = 5; 
    int ySize = 5;
    Mesh mesh;

    private float timer = 0;
    public float rate = 1;
    public float intensity = 0.5f;


   
    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh oMesh = meshFilter.sharedMesh;

        //make sure not to overwrite this mesh by copying, othere
        mesh = new Mesh(); 
        mesh.name = "Procedural Grid Y";
       // mesh.vertices = oMesh.vertices;
       // mesh.triangles = oMesh.triangles;
       // mesh.normals = oMesh.normals;
       // mesh.uv = oMesh.uv;
        meshFilter.mesh = mesh; 

        mesh.Clear();

       

        Vector3[] vertices = new Vector3[(numCellsX + 1) * (numCellsY + 1)];
        Vector2[] uv = new Vector2[vertices.Length];

        float startX = -xSize / 2.0f;
        float startY = -ySize / 2.0f;
        float xInc = (float)xSize / (float)numCellsX;
        float yInc = (float)ySize / (float)numCellsY;

        int idx = 0;
        for (int y = 0; y <= numCellsY; y++)
        {
            for (int x = 0; x <= numCellsX; x++)
            {
                float zVal = (Mathf.Sin(x) * intensity) / 2;
                vertices[idx] = new Vector3(startX + xInc * x, startY + yInc * y, zVal);
                uv[idx] = new Vector2((float)x / (float)numCellsX, (float)y / (float)numCellsY) ;
                idx++;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[numCellsX * numCellsY * 6];
        int t_idx = 0;
        int v_idx = 0;
        for (int y = 0; y < numCellsY; y++)
        {
            for (int x = 0; x < numCellsX; x++)
            {
                triangles[t_idx] = v_idx;
                triangles[t_idx + 3] = triangles[t_idx + 2] = v_idx + 1;
                triangles[t_idx + 4] = triangles[t_idx + 1] = v_idx + numCellsX + 1;
                triangles[t_idx + 5] = v_idx + numCellsX + 2;

                v_idx++;
                t_idx += 6;
            }
            v_idx++;
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); //much easier than doing it ourselves!


      

    }

    // Update is called once per frame
    void Update()
    {
        doWaterFlow();

        if(Input.GetKeyDown(KeyCode.A))
        {
            rate += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rate += -0.5f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            intensity += 0.3f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            intensity += -0.3f;
        }
    }

    void doWaterFlow()
    {
        timer += Time.deltaTime * rate;

        Vector3[] vertices = new Vector3[(numCellsX + 1) * (numCellsY + 1)];
        Vector2[] uv = new Vector2[vertices.Length];

        float startX = -xSize / 2.0f;
        float startY = -ySize / 2.0f;
        float xInc = (float)xSize / (float)numCellsX;
        float yInc = (float)ySize / (float)numCellsY;

        int idx = 0;
        for (int y = 0; y <= numCellsY; y++)
        {
            for (int x = 0; x <= numCellsX; x++)
            {
                float zVal = (Mathf.Sin(x + timer) * intensity) / 2;
                vertices[idx] = new Vector3(startX + xInc * x, startY + yInc * y, zVal);
                uv[idx] = new Vector2((float)x / (float)numCellsX, (float)y / (float)numCellsY);
                idx++;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[numCellsX * numCellsY * 6];
        int t_idx = 0;
        int v_idx = 0;
        for (int y = 0; y < numCellsY; y++)
        {
            for (int x = 0; x < numCellsX; x++)
            {
                triangles[t_idx] = v_idx;
                triangles[t_idx + 3] = triangles[t_idx + 2] = v_idx + 1;
                triangles[t_idx + 4] = triangles[t_idx + 1] = v_idx + numCellsX + 1;
                triangles[t_idx + 5] = v_idx + numCellsX + 2;

                v_idx++;
                t_idx += 6;
            }
            v_idx++;
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
