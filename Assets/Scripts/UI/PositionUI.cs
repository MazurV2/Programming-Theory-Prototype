using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PositionUI : MonoBehaviour
{
    [Header("Player Transform")]
    [SerializeField] private TransformVariableSO playerTransformSO;

    [Header("Game Bounds")]
    [SerializeField] private GameBoundsSO gameBoundsSO;

    [Header("UI Elements")]
    [SerializeField] private RawImage xRuler;
    [SerializeField] private RawImage xPositionIndicator;
    [SerializeField] private RawImage yRuler;
    [SerializeField] private RawImage yPositionIndicator;

    private float xRulerWidth;
    private float yRulerWidth;

    private void Start()
    {
        if (xRuler != null)
        {
            xRulerWidth = xRuler.rectTransform.rect.width;
        }

        if (yRuler != null)
        {
            yRulerWidth = yRuler.rectTransform.rect.width;
        }
    }

    private void Update()
    {
        if (playerTransformSO == null) return;

        float xPosition = Mathf.InverseLerp(gameBoundsSO.MinX, gameBoundsSO.MaxX, playerTransformSO.Value.position.x);
        float xUiPosition = Mathf.Lerp(-xRulerWidth/2, xRulerWidth/2, xPosition);

        float yPosition = Mathf.InverseLerp(gameBoundsSO.MinY, gameBoundsSO.MaxY, playerTransformSO.Value.position.y);
        float yUiPosition = Mathf.Lerp(-yRulerWidth/2, yRulerWidth/2, yPosition);

        if (xRuler != null && xPositionIndicator != null)
        {
            xPositionIndicator.rectTransform.anchoredPosition = new Vector2(xUiPosition, xPositionIndicator.rectTransform.anchoredPosition.y);
        }

        if (yRuler != null && yPositionIndicator != null)
        {
            yPositionIndicator.rectTransform.anchoredPosition = new Vector2(yUiPosition, yPositionIndicator.rectTransform.anchoredPosition.y);
        }
    }
}
