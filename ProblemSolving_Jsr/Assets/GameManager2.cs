using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    public GameObject greenCube;
    public GameObject redCube;
    public GameObject bulletPrefab;
    public Stack<GameObject> bulletList;
    public int bulletCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        // 스택을 초기화합니다.
        bulletList = new Stack<GameObject>();
        bulletCount = 0;

        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Push(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (bulletCount < 10)
        {
            GameObject bullet = bulletList.Pop();
            bullet.SetActive(true);
            bulletCount++;
        }
    }
}
