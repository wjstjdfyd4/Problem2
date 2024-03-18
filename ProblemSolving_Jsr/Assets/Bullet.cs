using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 총알이 활성화될 때 호출되는 메서드
    void OnEnable()
    {
        // 총알의 위치를 게임 매니저의 녹색 큐브 위치로 설정
        gameObject.transform.position = GameManager.instance.greenCube.transform.position;
    }

    // 매 프레임마다 호출되는 메서드
    void Update()
    {
        // 총알을 오른쪽으로 이동
        transform.Translate(Vector2.right * Time.deltaTime * 5f);
    }

    // 충돌이 발생했을 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        // 총알 비활성화
        gameObject.SetActive(false);

        // 활성화된 총알 개수 감소
        GameManager.instance.bulletCount--;

        // 총알을 다시 큐에 추가
        GameManager.instance.bulletList.Enqueue(gameObject);
    }
}
