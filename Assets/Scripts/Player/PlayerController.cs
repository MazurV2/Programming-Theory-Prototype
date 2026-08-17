using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    private Vector2 currentMovement;

    [SerializeField] private GameObject plane;

    [SerializeField] private HealthSystem healthSystem;

    [Header("Movement Parameters")]
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float baseTiltSpeed = 5f;

    [Space(5)]
    [SerializeField] private float maxTiltX = 45f;
    [SerializeField] private float maxTiltY = 45f;
    
    [Space(5)]
    [SerializeField] private GameBoundsSO gameBoundsSO;

    private void OnEnable()
    {
        if (inputReader != null) 
        { 
            inputReader.onMoveInput += UpdateMoveVector;
        }

        if (healthSystem != null)
        {
            healthSystem.ResetHealth();
            healthSystem.OnDied += Die;
        }
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.onMoveInput -= UpdateMoveVector;
        }

        if (healthSystem != null)
        {
            healthSystem.OnDied -= Die;
        }
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
        if (gameBoundsSO == null) return;
        transform.localPosition = gameBoundsSO.ClampPosition(transform.localPosition);
    }

    private void Tilt()
    {
        float pitch = -currentMovement.y * maxTiltY;
        float roll = -currentMovement.x * maxTiltX;

        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);

        plane.transform.localRotation = Quaternion.Slerp(plane.transform.localRotation, targetRotation, baseTiltSpeed * Time.deltaTime);
    }

    private void Die(DamageSource damageSource)
    {
        gameObject.SetActive(false);
    }
}