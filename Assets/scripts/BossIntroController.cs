using UnityEngine;
using System.Collections;

public class BossIntroController : MonoBehaviour
{
    public GameObject blackPanel;
    public GameObject redFlash;
    public RectTransform boss;

    public DialogueManager dialogueManager;
    public KeyboardController keyboardController;
    string[] intro =
    {
        "I know who you are.",
        "...",
        "I know what you're capable of.",
        "...",
        "You're the only one capable of deleting a virus.",
        "...",
        "But I only need time.",
        "...",
        "Time to reach the internet.",
        "...",
        "So I finished your game for you.",
        "...",
        "But you don't get to play it."
    };

    [Header("Boss Animation")]
    public Vector2 bossStartPosition;
    public Vector2 bossEndPosition;

    public float slideDuration = 0.6f;
    public float floatAmount = 3f;
    public float floatSpeed = 1.5f;
    private float currentFloatSpeed;
    private float currentFloatAmount;
    void Start()
    {
        blackPanel.SetActive(false);
        redFlash.SetActive(false);
        boss.gameObject.SetActive(false);
        dialogueManager.OnLineStarted += HandleDialogueLine;
        currentFloatSpeed = floatSpeed;
        currentFloatAmount = floatAmount;
    }

    public void StartBossIntro()
    {
        StartCoroutine(BossSequence());
    }
    public void EndIntro()
{
    blackPanel.SetActive(false);
    redFlash.SetActive(false);
    boss.gameObject.SetActive(false);
}

    IEnumerator BossSequence()
{
    blackPanel.SetActive(true);

    yield return new WaitForSeconds(0.35f);

    // More frequent digital flickers
    for (int i = 0; i < 5; i++)
    {
        redFlash.SetActive(true);
        yield return new WaitForSeconds(0.04f);

        redFlash.SetActive(false);
        yield return new WaitForSeconds(0.03f);
    }

    // Spawn boss off-screen
    boss.gameObject.SetActive(true);
    boss.anchoredPosition = bossStartPosition;

    // Slide boss in
    yield return StartCoroutine(SlideBoss());

    // Begin floating forever
    StartCoroutine(FloatBoss());

    // Small pause before speaking
    yield return new WaitForSeconds(0.5f);

    dialogueManager.StartDialogue("??????", intro);
}
IEnumerator SlideBoss()
{
    Vector2 direction = (bossEndPosition - bossStartPosition).normalized;
    Vector2 overshoot = bossEndPosition + direction * 20f;

    float timer = 0f;

    // Slide directly to overshoot
    while (timer < slideDuration)
    {
        timer += Time.deltaTime;

        float t = timer / slideDuration;
        t = 1 - Mathf.Pow(1 - t, 3);

        boss.anchoredPosition = Vector2.Lerp(
            bossStartPosition,
            overshoot,
            t);

        yield return null;
    }

    // Settle back to center
    timer = 0f;

    while (timer < 0.15f)
    {
        timer += Time.deltaTime;

        float t = timer / 0.15f;
        t = 1 - Mathf.Pow(1 - t, 3);

        boss.anchoredPosition = Vector2.Lerp(
            overshoot,
            bossEndPosition,
            t);

        yield return null;
    }

    boss.anchoredPosition = bossEndPosition;
}
IEnumerator FloatBoss()
{
    Vector2 startPos = bossEndPosition;

    while (true)
    {
        float y = Mathf.Sin(Time.time * currentFloatSpeed) * currentFloatAmount;

        boss.anchoredPosition =
            startPos + new Vector2(0, y);

        yield return null;
    }
}
void HandleDialogueLine(int line)
{
    switch (line)
    {
        case 12:
        currentFloatSpeed = 5f;
        currentFloatAmount = 4f;
        keyboardController.BeginPossession();

        Debug.Log("Angry float!");
        break;
    }
}
}