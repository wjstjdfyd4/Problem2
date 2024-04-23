using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 총알이 활성화될 때 호출되는 메서드
    void OnEnable()
    {
        // 총알의 위치를 게임 매니저의 녹색 큐브 위치로 설정
        transform.position = GameManager.instance.greenCube.transform.position;
    }

    // 매 프레임마다 호출되는 메서드
    void Update()
    {
        // 총알을 오른쪽으로 이동
        transform.Translate(Vector2.right * Time.deltaTime * 5f);

        // 총알이 빨간 큐브의 콜라이더와 충돌한 경우 처리
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("RedCube"))
            {
                // 총알 비활성화
                gameObject.SetActive(false);

                // 활성화된 총알 개수 감소
                GameManager.instance.bulletCount--;

                // 총알을 다시 큐에 추가
                GameManager.instance.bulletList.Enqueue(gameObject);
                break; // 다음 총알과의 충돌 체크를 하지 않기 위해 루프 종료
            }
        }
    }
}
