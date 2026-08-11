using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    private Vector2 currentMovement;

    [SerializeField] private GameObject plane;

    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float baseTiltSpeed = 5f;

    private float maxTiltX = 45f;
    private float maxTiltY = 45f;

    [SerializeField] private float xBoundary = 5f;
    [SerializeField] private float yBoundary = 5f;

    private void OnEnable()
    {
        inputReader.onMoveInput += UpdateMoveVector;
    }

    private void OnDisable()
    {
        inputReader.onMoveInput -= UpdateMoveVector;
    }

    private void Update()
    {
        Move();
        Tilt();
    }

    private void UpdateMoveVector(Vector2 movement)
    {
        currentMovement = movement;
    }

    private void Move()
    {
        transform.Translate(currentMovement * baseMoveSpeed * Time.deltaTime);
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 newPosition = transform.localPosition;

        newPosition.x = Mathf.Clamp(newPosition.x, -xBoundary, xBoundary);
        newPosition.y = Mathf.Clamp(newPosition.y, -yBoundary, yBoundary);

        transform.localPosition = newPosition;
    }

    private void Tilt()
    {
        float pitch = -currentMovement.y * maxTiltY;
        float roll = -currentMovement.x * maxTiltX;

        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);

        plane.transform.localRotation = Quaternion.Slerp(plane.transform.localRotation, targetRotation, baseTiltSpeed * Time.deltaTime);
    }
}
