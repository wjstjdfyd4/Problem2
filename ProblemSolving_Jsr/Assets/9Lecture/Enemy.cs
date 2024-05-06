using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Camera enemyCamera;
    public Material material1; // �������� ���ο� �ִ� ������Ʈ�� ����� ��Ƽ����
    public Material material2; // �������� �ۿ� �ִ� ������Ʈ�� ����� ��Ƽ����
    public GameObject player;

    public Vector3 patrolAreaCenter; // ���� ������ �߽�
    public float patrolRadius = 5.0f; // ���� ������ �ݰ�
    public float speed = 3.0f; // �̵� �ӵ�

    private Vector3 nextPosition; // ���� ��ǥ ��ġ
    private bool isGo = true;
    private Plane[] frustumPlanes; // �������� �÷��� �迭

    private void Start()
    {
        SetNextPosition();
    }

    private void Update()
    {
        if (GameSceneManager.instance.endGame) return;

        // ī�޶��� �������� �÷��� ���
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(enemyCamera);

        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            if (!renderer.gameObject.CompareTag("Player")) continue;

            Bounds bounds = renderer.bounds;

            if (GeometryUtility.TestPlanesAABB(frustumPlanes, bounds))
            {
                isGo = false;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                patrolRadius = 500f;
            }
            else
            {
                patrolRadius = 5f;
                if (isGo) MoveToNextPosition();
                else StartRotation();
            }
        }
    }

    private void SetNextPosition()
    {
        CalculateNextPosition();
        CancelInvoke(nameof(SetNextPosition));
        Invoke(nameof(SetNextPosition), Random.Range(3, 7));
    }

    private void CalculateNextPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection.y = 0;
        nextPosition = patrolAreaCenter + randomDirection;
    }

    private void MoveToNextPosition()
    {
        Vector3 moveDirection = nextPosition - transform.position;
        transform.position += moveDirection.normalized * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, nextPosition) < 0.5f)
        {
            SetNextPosition();
        }
    }

    private void StartRotation()
    {
        StartCoroutine(RotateOverTime(-90f, 3f));
    }

    private IEnumerator RotateOverTime(float angle, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, angle, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = finalRotation;

        if (angle < 0)
        {
            yield return StartCoroutine(RotateOverTime(180f, 3f));
        }

        isGo = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(patrolAreaCenter, patrolRadius);
    }
}
