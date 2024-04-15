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
    // ������ �븻 ���� �迭
    public Vector3[] vertexNormals;

    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            // ������� ���� 
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
            // �ո�
        0, 1, 2,
        2, 3, 4,
        4, 5, 6,
        6, 7, 8,
        8, 9, 0,
        6, 8, 0,
        0, 2, 6,
        4, 6, 2,

        // �޸�
        10, 11, 12,
        12, 13, 14,
        14, 15, 16,
        16, 17, 18,
        18, 19, 10,
        16, 18, 10,
        10, 12, 16,
        14, 16, 12,

        // �޸� �ڿ���
        12, 11, 10,
        10, 19, 18,
        18, 17, 16,
        16, 15, 14,
        14, 13, 12,
        10, 18, 16,
        16, 12, 10,
        12, 16, 14,

        // ���
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

        // �� ������ �븻 ���� ���
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

            // �� �ﰢ���� �븻 ���͸� ������ �븻 ���Ϳ� ����
            vertexNormals[indexA] += triangleNormal;
            vertexNormals[indexB] += triangleNormal;
            vertexNormals[indexC] += triangleNormal;
        }

        // ������ �븻 ���͸� ����ȭ
        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i] = vertexNormals[i].normalized;
        }

        // Mesh�� Normals �迭�� ����
        mesh.normals = vertexNormals;

        // MeshRenderer�� �߰��� �� mesh�� ����
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        // MeshFilter�� �߰��ϰ� mesh�� ����
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = mesh;
        // ���� ���� ����
        Vector3 lightDirection = new Vector3(1, -1, -1).normalized;

    }
}
