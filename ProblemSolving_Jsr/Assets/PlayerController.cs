using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    private float horizontalInput; // 수평 입력
    private float verticalInput; // 수직 입력
    private Vector3 moveDirection; // 이동 방향
    private Camera mainCamera; // 주 카메라

    void Start()
    {
        // 주 카메라 가져오기
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 이동 입력
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // 현재 카메라의 전방 방향과 좌우 방향을 고려하여 이동 방향 설정
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up).normalized;

        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // O 키를 눌렀을 때 y축으로 90도 회전
        if (Input.GetKeyDown(KeyCode.O))
        {
            transform.Rotate(Vector3.up, 90f);
            // 이동 방향도 함께 회전
            moveDirection = Quaternion.Euler(0f, 90f, 0f) * moveDirection;
        }

        // P 키를 눌렀을 때 y축으로 -90도 회전
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.Rotate(Vector3.up, -90f);
            // 이동 방향도 함께 회전
            moveDirection = Quaternion.Euler(0f, -90f, 0f) * moveDirection;
        }

        // 이동 적용
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
