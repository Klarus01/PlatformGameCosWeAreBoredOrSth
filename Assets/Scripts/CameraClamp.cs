using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(targetToFollow.position.x, -66f, 385f), Mathf.Clamp(targetToFollow.position.y, 8f, 100f), transform.position.z);
    }
}