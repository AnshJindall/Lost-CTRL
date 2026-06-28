using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DeathScreenController : MonoBehaviour
{
    public GameObject deathScreen;

    public Player2DMovement playerMovement;

    public Image skull;

    public Sprite skullClosed;
    public Sprite skullOpen;
    bool dead = false;

    void Start()
    {
        deathScreen.SetActive(false);
    }

    void Update()
    {
        if (!dead)
            return;

        if (Input.anyKeyDown)
        {
            Restart();
        }
    }

    public void Die()
    {
        if (dead)
            return;

        dead = true;

        playerMovement.enabled = false;

        deathScreen.SetActive(true);

        StartCoroutine(SkullAnimation());
    }

    IEnumerator SkullAnimation()
    {
        while(dead)
        {
            skull.sprite = skullClosed;

            yield return new WaitForSeconds(0.35f);

            skull.sprite = skullOpen;

            yield return new WaitForSeconds(0.35f);
        }
    }

    void Restart()
    {
        dead = false;

        StopAllCoroutines();

        deathScreen.SetActive(false);

        playerMovement.enabled = true;
    }
}