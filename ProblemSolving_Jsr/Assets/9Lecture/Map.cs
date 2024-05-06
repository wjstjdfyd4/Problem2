using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    public GameObject wall0;
    public GameObject wall1;
    public GameObject wall2;

    void Start()
    {
        // CSV 파일 경로
        string filePath = "Assets/9Lecture/data.csv";

        // 파일의 모든 라인을 읽기
        string[] lines = File.ReadAllLines(filePath);

        // 2차원 배열 생성 (행의 수는 라인 수, 열의 수는 첫 번째 라인에서 쉼표로 분리된 요소의 수)
        string[,] data = new string[lines.Length, lines[0].Split(',').Length];

        // 각 라인을 처리
        for (int i = 0; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');

            // 각 열을 처리
            for (int j = 0; j < columns.Length; j++)
            {
                data[i, j] = columns[j];
            }
        }

        float x = 17.6f / 2f - 5.5f;
        float z = 18.6f / 2f - 5.5f;

        // 데이터 출력 (옵션)
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                GameObject wallObejct;

                switch (data[i, j])
                {
                    case "0":
                        wallObejct = Instantiate(wall0);
                        wallObejct.transform.position = new Vector3(x, 0.5f, z);
                        break;
                    case "1":
                        wallObejct = Instantiate(wall1);
                        wallObejct.transform.position = new Vector3(x, 0.5f, z);
                        break;
                    case "2":
                        wallObejct = Instantiate(wall2);
                        wallObejct.transform.position = new Vector3(x, 1f, z);
                        break;
                }
                x += 1f;
            }
            x = 17.6f / 2f - 5.5f;
            z += 1f;
        }
    }
}
