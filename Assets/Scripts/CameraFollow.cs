using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetToFollow;
    private Vector3 offset;
    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime = 0.25f;

    [Header("Bounds Parameters")]
    [SerializeField] private GameBoundsSO gameBoundsSO;
    [SerializeField] private float cameraBoundsScale = 0.8f;

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
        if (gameBoundsSO == null) return;
        transform.position = gameBoundsSO.ClampPositionScaled(transform.position, cameraBoundsScale);
    }
}
