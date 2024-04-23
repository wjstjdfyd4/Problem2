using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;                // 카메라가 따라다닐 타겟
    public float offsetY = 10.0f;           // 카메라의 y좌표
    public float offsetZ = -10.0f;          // 카메라의 z좌표
    public float rotationAmount = 90f;      // 회전 각도
    public float rotationDuration = 1f;     // 회전 지속 시간

    private bool isRotating = false;        // 회전 중 여부
    private Quaternion targetRotation;      // 목표 회전 각도
    private Quaternion initialRotation;     // 초기 회전 각도
    private float rotationTimer = 0f;       // 회전 타이머

    void Update()
    {
        // O 키를 누르면 시계 방향으로 90도 회전
        if (Input.GetKeyDown(KeyCode.O) && !isRotating)
        {
            RotateCamera(rotationAmount);
        }
        // P 키를 누르면 반시계 방향으로 90도 회전
        else if (Input.GetKeyDown(KeyCode.P) && !isRotating)
        {
            RotateCamera(-rotationAmount);
        }

        // 회전 애니메이션 처리
        if (isRotating)
        {
            rotationTimer += Time.deltaTime;

            float t = Mathf.Clamp01(rotationTimer / rotationDuration);
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

            // 타겟 주위로 이동
            Vector3 desiredPosition = target.position + Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * new Vector3(0f, offsetY, offsetZ);
            transform.position = desiredPosition;

            if (rotationTimer >= rotationDuration)
            {
                isRotating = false;
                rotationTimer = 0f;
            }
        }
    }

    // 회전 애니메이션 시작
    void RotateCamera(float angle)
    {
        // 회전 중 플래그 설정
        isRotating = true;

        // 초기 회전 각도 저장
        initialRotation = transform.rotation;

        // 목표 회전 각도 설정
        targetRotation = Quaternion.AngleAxis(angle, Vector3.up) * transform.rotation;
    }
}
