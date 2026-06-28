using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathScreenController : MonoBehaviour
{
    public GameObject deathScreen;

    public Player2DMovement playerMovement;
    public PlayerHealth playerHealth;

    public Image skull;
    public Sprite skullClosed;
    public Sprite skullOpen;
    private bool canRestart = false;
    private bool dead = false;
    public CanvasGroup deathCanvasGroup;
    void Start()
    {
        deathCanvasGroup.alpha = 0;
        deathScreen.SetActive(false);
    }

    void Update()
    {
        if (!dead || !canRestart)
            return;

        if (Input.anyKeyDown)
        {
            Restart();
        }
    }

    public void Die()
    {
        if (dead) return;

        dead = true;
        canRestart = false;

        playerMovement.enabled = false;
        deathScreen.SetActive(true);
        StartCoroutine(DeathSequence());
    }

    IEnumerator SkullAnimation()
    {
        while (dead)
        {
            skull.sprite = skullClosed;
            yield return new WaitForSeconds(0.35f);

            skull.sprite = skullOpen;
            yield return new WaitForSeconds(0.35f);
        }
    }
    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(3f);

        canRestart = true;
    }
    void Restart()
    {
        dead = false;

        StopAllCoroutines();

        deathScreen.SetActive(false);

        // Reset player health
        playerHealth.health = 100;

        playerMovement.enabled = true;

        // Existing respawn logic will go here later.
    }
    IEnumerator DeathSequence()
    {
        // Small freeze when dying
        yield return new WaitForSeconds(0.2f);

        deathScreen.SetActive(true);

        // Corrupted monitor flicker
        deathCanvasGroup.alpha = 0f;
        yield return new WaitForSeconds(0.03f);

        deathCanvasGroup.alpha = 0.7f;
        yield return new WaitForSeconds(0.05f);

        deathCanvasGroup.alpha = 0.2f;
        yield return new WaitForSeconds(0.04f);

        deathCanvasGroup.alpha = 1f;

        StartCoroutine(SkullAnimation());
        StartCoroutine(RestartDelay());
    }
}