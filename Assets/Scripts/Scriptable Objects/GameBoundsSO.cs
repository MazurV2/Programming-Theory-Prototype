using UnityEngine;

[CreateAssetMenu(fileName = "GameBoundsSO", menuName = "Game/Game Bounds")]
public class GameBoundsSO : ScriptableObject
{
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    public float MinX => minX;
    public float MaxX => maxX;
    public float MinY => minY;
    public float MaxY => maxY;

    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3(
            position.x = Mathf.Clamp(position.x, minX, maxX),
            position.y = Mathf.Clamp(position.y, minY, maxY),
            position.z
        );
    }

    public Vector3 ClampPositionScaled(Vector3 position, float scale)
    {
        return new Vector3(
            position.x = Mathf.Clamp(position.x, minX * scale, maxX * scale),
            position.y = Mathf.Clamp(position.y, minY * scale, maxY * scale),
            position.z
        );
    }
}
