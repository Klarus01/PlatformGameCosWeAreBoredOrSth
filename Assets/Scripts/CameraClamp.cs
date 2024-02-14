using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private float followSpeed = 5f;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -66f, 385f),
            Mathf.Clamp(targetToFollow.position.y, 8f, 100f),
            transform.position.z
        );

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        transform.position = newPosition;
    }
}