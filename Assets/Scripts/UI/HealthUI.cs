using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Header("Health Variable Scriptable Objects")]
    [SerializeField] private IntVariableSO currentHealthSO;
    [SerializeField] private IntVariableSO maxHealthSO;

    [Header("Health UI Elements")]
    [SerializeField] private TMP_Text healthText;

    private void OnEnable()
    {
        if (currentHealthSO != null)
        {
            currentHealthSO.OnValueChanged += UpdateHealthUI;

            UpdateHealthUI(currentHealthSO.Value);
        }
    }

    private void OnDisable()
    {
        if (currentHealthSO != null)
        {
            currentHealthSO.OnValueChanged -= UpdateHealthUI;
        }
    }

    private void UpdateHealthUI(int value)
    {
        if (healthText == null) return;

        if (maxHealthSO != null)
        {
            healthText.text = $"Lives: {value.ToString()}/{maxHealthSO.Value}";
        }
        else
        {
            healthText.text = $"Lives: {value.ToString()}";
        }
    } 

}
