using UnityEngine;

public class PromptManager : MonoBehaviour
{
    public static PromptManager Instance;

    public GameObject ePrompt;
    public GameObject lockedPrompt;
    public bool uiOpen = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowE()
{
    if (uiOpen)
        return;

    ePrompt.SetActive(true);
    lockedPrompt.SetActive(false);
}

    public void ShowLocked()
{
    if (uiOpen)
        return;

    lockedPrompt.SetActive(true);
    ePrompt.SetActive(false);
}

    public void Hide()
    {
        ePrompt.SetActive(false);
        lockedPrompt.SetActive(false);
    }
}