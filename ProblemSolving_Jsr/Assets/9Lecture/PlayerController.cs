using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera; // 플레이어 카메라를 위한 public 변수
    public float rotationSpeed = 90.0f; // 초당 회전 속도
    private float targetAngle = 0; // 목표 각도
    private bool isRotating = false; // 회전 중인지 확인하는 플래그
    public TextMeshProUGUI text;

    public float speed = 5.0f; // 플레이어의 이동 속도

    private void Start()
    {
        // 카메라가 지정되지 않았을 경우, 메인 카메라를 자동으로 찾아 할당
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (GameSceneManager.instance.endGame) return;

        // 플레이어의 이동 입력을 받습니다.
        float horizontal = Input.GetAxis("Horizontal"); // A, D 키
        float vertical = Input.GetAxis("Vertical"); // W, S 키

        // 카메라의 방향에 따라 이동 방향을 계산합니다.
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // 카메라가 바라보는 방향의 Y 성분을 제거하여 수평 이동만 고려하게 합니다.
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 최종 이동 벡터를 계산합니다.
        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // 이동 벡터에 따라 플레이어를 이동시킵니다.
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // 카메라 회전 입력 처리
        if (Input.GetKeyDown(KeyCode.O))
        {
            RotateCamera(-90);
            playerCamera.transform.LookAt(transform);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            RotateCamera(90);
            playerCamera.transform.LookAt(transform);
        }

        if (isRotating)
        {
            PerformRotation();
        }

        //playerCamera.transform.LookAt(transform);
    }

    void RotateCamera(float angle)
    {
        if (isRotating)
            return;

        targetAngle += angle;
        isRotating = true;
        StartCoroutine(RotateOverTime(angle));
    }

    IEnumerator RotateOverTime(float angle)
    {
        float startAngle = playerCamera.transform.eulerAngles.y;
        float endAngle = startAngle + angle;
        float timeElapsed = 0;

        while (timeElapsed < 1)
        {
            float angleToRotate = Mathf.Lerp(startAngle, endAngle, timeElapsed / 1);
            playerCamera.transform.RotateAround(transform.position, Vector3.up, angleToRotate - playerCamera.transform.eulerAngles.y);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.RotateAround(transform.position, Vector3.up, endAngle - playerCamera.transform.eulerAngles.y);
        isRotating = false;
    }

    void PerformRotation()
    {
        if (Mathf.Abs(playerCamera.transform.eulerAngles.y - targetAngle) > 0.01f)
        {
            float step = rotationSpeed * Time.deltaTime;
            float angle = Mathf.MoveTowardsAngle(playerCamera.transform.eulerAngles.y, targetAngle, step);
            playerCamera.transform.RotateAround(transform.position, Vector3.up, angle - playerCamera.transform.eulerAngles.y);
        }
        else
        {
            isRotating = false;
            playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, targetAngle, playerCamera.transform.eulerAngles.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedCube"))
        {
            GameSceneManager.instance.endGame = true;
            text.text = "CLEAR!";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameSceneManager.instance.endGame = true;
            text.text = "Game Over!";
        }
    }
}