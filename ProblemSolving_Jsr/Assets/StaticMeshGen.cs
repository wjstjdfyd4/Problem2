using System.Collections;
using System.Collections.Generic;
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
    // 정점의 노말 벡터 배열
    public Vector3[] vertexNormals;

    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            // 별모양의 정점 
            new Vector3 (-1.0f, -2.0f, 0.0f), //0
            new Vector3 (0.0f, 0.0f, 0.0f), //1    
            new Vector3 (1.0f, -2.0f, 0.0f), //2
            new Vector3 (3.5f, -2.0f, 0.0f), //3
            new Vector3 (1.5f, -3.5f, 0.0f), //4
            new Vector3 (2.5f, -6.0f, 0.0f), //5
            new Vector3 (0f, -4.5f, 0.0f), //6
            new Vector3 (-2.5f, -6.0f, 0.0f), //7
            new Vector3 (-1.5f, -3.5f, 0.0f), //8
            new Vector3 (-3.5f, -2.0f, 0.0f), //9
            
            new Vector3 (-1.0f, -2.0f, 4.0f), //10
            new Vector3 (0.0f, 0.0f, 4.0f), //11
            new Vector3 (1.0f, -2.0f, 4.0f), //12
            new Vector3 (3.5f, -2.0f, 4.0f), //13
            new Vector3 (1.5f, -3.5f, 4.0f), //14
            new Vector3 (2.5f, -6.0f, 4.0f), //15
            new Vector3 (0f, -4.5f, 4.0f), //16
            new Vector3 (-2.5f, -6.0f, 4.0f), //17
            new Vector3 (-1.5f, -3.5f, 4.0f), //18
            new Vector3 (-3.5f, -2.0f, 4.0f), //19
        };

        mesh.vertices = vertices;

        int[] TR = new int[]
        {
            // 앞면
        0, 1, 2,
        2, 3, 4,
        4, 5, 6,
        6, 7, 8,
        8, 9, 0,
        6, 8, 0,
        0, 2, 6,
        4, 6, 2,

        // 뒷면
        10, 11, 12,
        12, 13, 14,
        14, 15, 16,
        16, 17, 18,
        18, 19, 10,
        16, 18, 10,
        10, 12, 16,
        14, 16, 12,

        // 뒷면 뒤에서
        12, 11, 10,
        10, 19, 18,
        18, 17, 16,
        16, 15, 14,
        14, 13, 12,
        10, 18, 16,
        16, 12, 10,
        12, 16, 14,

        // 기둥
        1, 11, 12,
        12, 2, 1,
        2, 12, 13,
        13, 3, 2,
        3, 13, 14,
        14, 4, 3,
        4, 14, 15,
        15, 5, 4,
        5, 15, 16,
        16, 6, 5,
        6, 16, 17,
        17, 7, 6,
        7, 17, 18,
        18, 8, 7,
        8, 18, 19,
        19, 9, 8,
        9, 19, 10,
        10, 0, 9,
        0, 10, 11,
        11, 1, 0,

        };

        mesh.triangles = TR;

        // 각 정점의 노말 벡터 계산
        vertexNormals = new Vector3[vertices.Length];
        for (int i = 0; i < TR.Length; i += 3)
        {
            int indexA = TR[i];
            int indexB = TR[i + 1];
            int indexC = TR[i + 2];

            Vector3 vertexA = vertices[indexA];
            Vector3 vertexB = vertices[indexB];
            Vector3 vertexC = vertices[indexC];

            Vector3 triangleNormal = Vector3.Cross(vertexB - vertexA, vertexC - vertexA);

            // 각 삼각형의 노말 벡터를 정점의 노말 벡터에 더함
            vertexNormals[indexA] += triangleNormal;
            vertexNormals[indexB] += triangleNormal;
            vertexNormals[indexC] += triangleNormal;
        }

        // 정점의 노말 벡터를 정규화
        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i] = vertexNormals[i].normalized;
        }

        // Mesh의 Normals 배열에 설정
        mesh.normals = vertexNormals;

        // MeshRenderer를 추가한 후 mesh를 설정
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        // MeshFilter를 추가하고 mesh를 설정
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = mesh;
        // 빛의 방향 설정
        Vector3 lightDirection = new Vector3(1, -1, -1).normalized;

    }
}
