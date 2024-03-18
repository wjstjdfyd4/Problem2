using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ���� �Ŵ����� �ν��Ͻ��� �����ϴ� ���� ����
    public GameObject greenCube; // ��� ť�� ������
    public GameObject bulletPrefab; // �Ѿ� ������
    public Mc.Queue<GameObject> bulletList; // �Ѿ��� �����ϴ� ť
    public int bulletCount; // ���� Ȱ��ȭ�� �Ѿ��� ����

    // ���� �Ŵ����� �ν��Ͻ� ����
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // �ν��Ͻ��� ������ �ڱ� �ڽ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �ִ� ��� ���� ���� ������Ʈ �ı�
        }
    }

    // ���� ���� �� ����Ǵ� �޼���
    void Start()
    {
        bulletList = new Mc.Queue<GameObject>(); // �Ѿ��� �����ϴ� ť ����
        bulletCount = 0; // �ʱ�ȭ

        // �Ѿ��� �����տ��� �����Ͽ� ť�� �߰�
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab); // �Ѿ� ����
            bullet.SetActive(false); // ��Ȱ��ȭ ���·� ����
            bulletList.Enqueue(bullet); // ť�� �Ѿ� �߰�
        }
    }

    // �����Ӹ��� ����Ǵ� �޼���
    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϸ� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    // �Ѿ� �߻� �޼���
    void FireBullet()
    {
        if (bulletCount < 10) // Ȱ��ȭ�� �Ѿ��� 10�� �̸��� ��쿡�� �߻�
        {
            GameObject bullet = bulletList.Dequeue(); // ť���� �Ѿ��� ������
            bullet.SetActive(true); // �Ѿ��� Ȱ��ȭ ���·� ����
            bulletCount++; // Ȱ��ȭ�� �Ѿ� ���� ����
        }
    }
}
