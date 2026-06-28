using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SafeInteract : MonoBehaviour
{
    
    
    public GameObject safeUI;

    public Player2DMovement playerMovement;

    public SpriteRenderer safeSpriteRenderer;
    public Sprite openSafeSprite;

    public bool unlocked = false;

    private bool playerNearby;
    public Image enterKey;
    public ParticleSystem enterParticles;
    private bool canCollect = false;
    private bool enterCollected = false;
    public GameObject enterPrompt;

    void Update()
{
    // TEMP TEST
    if (Input.GetKeyDown(KeyCode.P))
    {
        enterParticles.Play();
    }

    // ---------------- SAFE UI ----------------
    if (safeUI.activeSelf)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSafe();
        }

        if (safeUI.activeSelf)
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("Pressed E");
        Debug.Log("canCollect = " + canCollect);
        Debug.Log("enterCollected = " + enterCollected);

        if (canCollect && !enterCollected)
        {   
           
            Debug.Log("Starting Coroutine");
            StartCoroutine(CollectEnter());
        }
    }
}

        return;
    }

    // ---------------- WORLD ----------------
    if (!playerNearby)
        return;

    if (Input.GetKeyDown(KeyCode.E) && !safeUI.activeSelf)
{
    if (!unlocked || enterCollected)
        return;

    OpenSafe();
}
}

    void OpenSafe()
{
    safeUI.SetActive(true);
    PromptManager.Instance.uiOpen = true;
    PromptManager.Instance.Hide();
    playerMovement.enabled = false;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    enterKey.enabled = true;
    enterCollected = false;
    canCollect = false;
    StartCoroutine(OpenSafeSequence());
}

    void CloseSafe()
    {
        safeUI.SetActive(false);
        PromptManager.Instance.uiOpen = false;

        if (playerNearby)
            PromptManager.Instance.ShowE();
        playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerNearby)
            PromptManager.Instance.ShowE();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNearby = true;

        if (unlocked && !enterCollected)
        {
            PromptManager.Instance.ShowE();
        }
        else
        {
            PromptManager.Instance.ShowLocked();
                   }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNearby = false;

        Invoke(nameof(HidePrompt), 0.02f);       
    }

    public void UnlockSafe()
    {
        unlocked = true;

        // Change the safe sprite
        if (safeSpriteRenderer != null && openSafeSprite != null)
        {
            safeSpriteRenderer.sprite = openSafeSprite;
        }

        if (playerNearby)
            PromptManager.Instance.ShowE();
    }
    void HidePrompt()
{
    PromptManager.Instance.Hide();
}
    IEnumerator CollectEnter()
{
     Debug.Log("Coroutine Started");
    enterCollected = true;
    canCollect = false;
    
    yield return new WaitForSeconds(0.2f);
    // Burst
    enterParticles.Play();

    // Let particles start from the key
    yield return new WaitForSeconds(0.18f);

    // Hide key sprite
    enterKey.enabled = false;

    // Let particles finish
    yield return new WaitForSeconds(1f);

    CloseSafe();
    enterPrompt.SetActive(false);
    Debug.Log("ENTER COLLECTED");
}
IEnumerator OpenSafeSequence()
{
    yield return new WaitForSeconds(0.35f);

    canCollect = true;
    enterPrompt.SetActive(true);
    Debug.Log("Can Collect = TRUE");

    PromptManager.Instance.ShowE();
}
    IEnumerator EnableCollect()
{
    yield return new WaitForSeconds(0.2f);
    canCollect = true;
}
}