using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetToFollow;
    private Vector3 offset;
    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime = 0.25f;

    [SerializeField] private float xBoundary = 3f;
    [SerializeField] private float yBoundary = 3f;

    private void Start()
    {
        if (targetToFollow != null)
        {
            offset = transform.position - targetToFollow.transform.position;
        }
    }

    void LateUpdate()
    {
        FollowTarget();
        ClampPosition();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = targetToFollow.transform.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private void ClampPosition()
    {
        Vector3 newPosition = transform.position;

        newPosition.x = Mathf.Clamp(newPosition.x, -xBoundary, xBoundary);
        newPosition.y = Mathf.Clamp(newPosition.y, -yBoundary, yBoundary);

        transform.position = newPosition;
    }
}
