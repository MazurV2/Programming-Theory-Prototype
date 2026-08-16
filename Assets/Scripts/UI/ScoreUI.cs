using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("Score Variable Scriptable Objects")]
    [SerializeField] private IntVariableSO scoreSO;

    [Header("Score UI Elements")]
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        if (scoreSO != null)
        {
            scoreSO.OnValueChanged += UpdateScoreUI;
            UpdateScoreUI(scoreSO.Value);
        }
    }

    private void OnDisable()
    {
        if (scoreSO != null)
        {
            scoreSO.OnValueChanged -= UpdateScoreUI;
        }
    }

    private void UpdateScoreUI(int value)
    {
        if (scoreText == null) return;

        scoreText.text = $"Score: {value.ToString("D4")}";
    }
}
