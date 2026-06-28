using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;
    public Image healthFill;
    public TMP_Text statusText;

    [Header("Settings")]
    public float maxHealth = 100f;
    public float smoothSpeed = 8f;

    private float currentFill;

    void Start()
    {
        currentFill = 1f;
        healthFill.fillAmount = 1f;
        statusText.text = "INTEGRITY";
    }

    void Update()
    {
        float targetFill = playerHealth.health / maxHealth;

        // Smoothly animate the bar
        currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * smoothSpeed);
        healthFill.fillAmount = currentFill;

        UpdateUI(targetFill);
    }

    void UpdateUI(float healthPercent)
    {
        // HEALTH COLORS
        if (healthPercent > 0.6f)
        {
            healthFill.color = Color.green;
            statusText.text = "STABLE";
        }
        else if (healthPercent > 0.3f)
        {
            healthFill.color = Color.yellow;
            statusText.text = "DAMAGED";
        }
        else if (healthPercent > 0.1f)
        {
            healthFill.color = new Color(1f, 0.55f, 0f); // Orange
            statusText.text = "CRITICAL";
        }
        else
        {
            healthFill.color = Color.red;
            statusText.text = "FAILURE";
        }
    }
}