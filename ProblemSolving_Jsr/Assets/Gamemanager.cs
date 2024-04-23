using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 게임 매니저의 인스턴스를 저장하는 정적 변수
    public GameObject greenCube; // 녹색 큐브 프리팹
    public GameObject bulletPrefab; // 총알 프리팹
    public Mc.Queue<GameObject> bulletList; // 총알을 관리하는 큐
    public int bulletCount; // 현재 활성화된 총알의 개수

    // 게임 매니저의 인스턴스 설정
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // 인스턴스가 없으면 자기 자신을 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있는 경우 현재 게임 오브젝트 파괴
        }
    }

    // 게임 시작 시 실행되는 메서드
    void Start()
    {
        bulletList = new Mc.Queue<GameObject>(); // 총알을 관리하는 큐 생성
        bulletCount = 0; // 초기화

        // 총알을 프리팹에서 생성하여 큐에 추가
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab); // 총알 생성
            bullet.SetActive(false); // 비활성화 상태로 설정
            bulletList.Enqueue(bullet); // 큐에 총알 추가
        }
    }

    // 프레임마다 실행되는 메서드
    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    // 총알 발사 메서드
    void FireBullet()
    {
        if (bulletCount < 10) // 활성화된 총알이 10개 미만인 경우에만 발사
        {
            GameObject bullet = bulletList.Dequeue(); // 큐에서 총알을 가져옴
            bullet.SetActive(true); // 총알을 활성화 상태로 변경
            bulletCount++; // 활성화된 총알 개수 증가
        }
    }
}