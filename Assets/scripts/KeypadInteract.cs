using UnityEngine;

public class KeypadInteract : MonoBehaviour
{
    
    public GameObject keypadUI;
    public Player2DMovement playerMovement;
    public static bool keypadOpen = false;
    private bool playerNearby;
    public fpscontroller fpsController;
    public bool solved = false;

    void Update()
    {
        if (playerNearby && !keypadUI.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
                if (solved)
                    return;
            keypadUI.SetActive(true);
            playerMovement.enabled = false;
            PromptManager.Instance.Hide();            
            keypadOpen = true;
            PromptManager.Instance.uiOpen = true;
        }

        if (keypadUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            keypadUI.SetActive(false);
            playerMovement.enabled = true;
            keypadOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (!keypadUI.activeSelf && !solved)
    PromptManager.Instance.ShowE();
    }}

    public void CloseKeypad()
    {
        solved = true;
        PromptManager.Instance.uiOpen = false;
        keypadUI.SetActive(false);
        playerMovement.enabled = true;
        keypadOpen = false;
        if (playerNearby && !solved)
    PromptManager.Instance.ShowE();}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Invoke(nameof(HidePrompt), 0.02f);        }
    }
    void HidePrompt()
{
    PromptManager.Instance.Hide();
}
}