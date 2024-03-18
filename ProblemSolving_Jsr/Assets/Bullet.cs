using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �Ѿ��� Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    void OnEnable()
    {
        // �Ѿ��� ��ġ�� ���� �Ŵ����� ��� ť�� ��ġ�� ����
        gameObject.transform.position = GameManager.instance.greenCube.transform.position;
    }

    // �� �����Ӹ��� ȣ��Ǵ� �޼���
    void Update()
    {
        // �Ѿ��� ���������� �̵�
        transform.Translate(Vector2.right * Time.deltaTime * 5f);
    }

    // �浹�� �߻����� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        // �Ѿ� ��Ȱ��ȭ
        gameObject.SetActive(false);

        // Ȱ��ȭ�� �Ѿ� ���� ����
        GameManager.instance.bulletCount--;

        // �Ѿ��� �ٽ� ť�� �߰�
        GameManager.instance.bulletList.Enqueue(gameObject);
    }
}
