using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }

    }
}

public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (0.0f, 0.0f, 0.0f), //0
            new Vector3 (-1.0f, -2.0f, 0.0f), //1
            new Vector3 (1.0f, -2.0f, 0.0f), //2
            new Vector3 (3.5f, -2.0f, 0.0f), //3
            new Vector3 (1.5f, -3.5f, 0.0f), //4
            new Vector3 (2.5f, -6.0f, 0.0f), //5
            new Vector3 (0f, -4.5f, 0.0f), //6
            new Vector3 (-2.5f, -6.0f, 0.0f), //7
            new Vector3 (-1.5f, -3.5f, 0.0f), //8
            new Vector3 (-3.5f, -2.0f, 0.0f), //9

            new Vector3 (0.0f, 0.0f, 10.0f), //10
            new Vector3 (-1.0f, -2.0f, 10.0f), //11
            new Vector3 (1.0f, -2.0f, 10.0f), //12
            new Vector3 (3.5f, -2.0f, 10.0f), //13
            new Vector3 (1.5f, -3.5f, 10.0f), //14
            new Vector3 (2.5f, -6.0f, 10.0f), //15
            new Vector3 (0f, -4.5f, 10.0f), //16
            new Vector3 (-2.5f, -6.0f, 10.0f), //17
            new Vector3 (-1.5f, -3.5f, 10.0f), //18
            new Vector3 (-3.5f, -2.0f, 10.0f), //19

        };

        mesh.vertices = vertices;

        int[] triangleIndices = new int[]
        {
            0,2,1,
            2,3,4,
            4,5,6,
            6,7,8,
            8,9,1,
            6,8,1,
            1,2,6,
            4,6,2,
            10,12,11,
            12,13,14,
            14,15,16,
            16,17,18,
            18,19,11,
            16,18,11,
            11,12,16,
            14,16,12,

            1,2,0,
            4,3,2,
            6,5,4,
            8,7,6,
            1,9,8,
            1,8,6,
            6,2,1,
            6,4,2,
            11,12,10,
            14,13,12,
            16,15,14,
            18,17,16,
            11,19,18,
            11,18,16,
            16,12,11,
            12,16,14,


           0,10,12,
           12,2,0,
           2,12,13,
           13,3,2,
           3,13,14,
           14,4,3,
           4,14,15,
           15,5,4,
           5,15,16,
           16,6,5,
           6,16,17,
           17,7,6,
           7,17,18,
           18,8,7,
           8,18,19,
           19,9,8,
           9,19,11,
           11,1,9,
           1,11,10,
           10,0,1,


           12,10,0,
           0,2,12,
           13,12,2,
           2,3,13,
           14,13,3,
           3,4,14,
           15,14,4,
           4,5,15,
           16,15,5,
           5,6,16,
           17,16,6,
           6,7,17,
           18,17,7,
           7,8,18,
           19,18,8,
           8,9,19,
           11,19,9,
           9,19,11,
           9,1,11,
           10,11,1,
           1,0,10,
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}