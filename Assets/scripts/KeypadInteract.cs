using UnityEngine;

public class KeypadInteract : MonoBehaviour
{
    public GameObject ePrompt;
    public GameObject keypadUI;
    public Player2DMovement playerMovement;
    public static bool keypadOpen = false;
    private bool playerNearby;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            keypadUI.SetActive(true);
            playerMovement.enabled = false;
            ePrompt.SetActive(false);
            keypadOpen = true;
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

            if (!keypadUI.activeSelf)
                ePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            ePrompt.SetActive(false);
        }
    }
}