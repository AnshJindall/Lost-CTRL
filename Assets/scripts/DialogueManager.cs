using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    // =========================
    // UI REFERENCES
    // =========================

    [Header("UI")]
    public RectTransform dialoguePanel;
    public TMP_Text speakerName;
    public TMP_Text dialogueText;
    public TMP_Text continueText;

    // =========================
    // PANEL ANIMATION
    // =========================

    [Header("Panel Animation")]
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;
    public float slideSpeed = 8f;

    // =========================
    // TYPEWRITER
    // =========================

    [Header("Typewriter")]
    public float typingSpeed = 0.03f;

    // =========================
    // AUTO ADVANCE
    // =========================

    [Header("Auto Advance")]
    public bool autoAdvance = true;
    public float autoAdvanceDelay = 5f;

    // =========================
    // DIALOGUE DATA
    // =========================

    private string[] dialogueLines;
    private int currentLine;

    // =========================
    // STATES
    // =========================

    private bool dialogueActive = false;
    private bool isTyping = false;

    // =========================
    // COROUTINES
    // =========================

    private Coroutine typingCoroutine;
    private Coroutine blinkCoroutine;
    private Coroutine autoAdvanceCoroutine;

    // Called after the final line.
    public Action OnDialogueFinished;
    public Action<int> OnLineStarted;

    // =========================================================

    void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!dialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // If we're still typing,
            // instantly finish this sentence.
            if (isTyping)
            {
                FinishTyping();
            }
            else
            {
                NextLine();
            }
        }
    }

    // =========================================================
    // START DIALOGUE
    // =========================================================

    // Multiple lines.
    public void StartDialogue(string speaker, string[] lines)
    {
        speakerName.text = speaker;

        dialogueLines = lines;
        currentLine = 0;

        dialogueActive = true;

        dialoguePanel.gameObject.SetActive(true);

        StartCoroutine(StartDialogueRoutine());
    }

    // Single line overload.
    public void StartDialogue(string speaker, string line)
    {
        StartDialogue(speaker, new string[] { line });
    }

    IEnumerator StartDialogueRoutine()
    {
        yield return SlidePanel(shownPosition);

        StartTyping();
    }

    // =========================================================
    // TYPEWRITER
    // =========================================================

    void StartTyping()
    {
        HideContinue();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        dialogueText.text = "";

        string line = dialogueLines[currentLine];
        OnLineStarted?.Invoke(currentLine);

        foreach (char c in line)
        {
            dialogueText.text += c;

            // TODO:
            // Play typing SFX every 2-3 characters.

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        ShowContinue();

        if (autoAdvance)
        {
            autoAdvanceCoroutine = StartCoroutine(AutoAdvance());
        }
    }

    // Instantly finish the current sentence.
    void FinishTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = dialogueLines[currentLine];

        isTyping = false;

        ShowContinue();

        if (autoAdvance)
        {
            autoAdvanceCoroutine = StartCoroutine(AutoAdvance());
        }
    }

    // =========================================================
    // NEXT LINE
    // =========================================================

    void NextLine()
    {
        CancelAutoAdvance();

        HideContinue();

        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            StartCoroutine(EndDialogueRoutine());
            return;
        }

        StartTyping();
    }

    // =========================================================
    // AUTO ADVANCE
    // =========================================================

    IEnumerator AutoAdvance()
    {
        yield return new WaitForSeconds(autoAdvanceDelay);

        if (!isTyping && dialogueActive)
        {
            NextLine();
        }
    }

    void CancelAutoAdvance()
    {
        if (autoAdvanceCoroutine != null)
        {
            StopCoroutine(autoAdvanceCoroutine);
            autoAdvanceCoroutine = null;
        }
    }

    // =========================================================
    // CONTINUE INDICATOR
    // =========================================================

    void ShowContinue()
    {
        continueText.gameObject.SetActive(true);

        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        blinkCoroutine = StartCoroutine(BlinkArrow());
    }

    void HideContinue()
    {
        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        continueText.gameObject.SetActive(false);
    }

    IEnumerator BlinkArrow()
    {
        while (true)
        {
            continueText.enabled = !continueText.enabled;

            yield return new WaitForSeconds(0.4f);
        }
    }

    // =========================================================
    // END DIALOGUE
    // =========================================================

    IEnumerator EndDialogueRoutine()
    {
        dialogueActive = false;

        HideContinue();

        yield return SlidePanel(hiddenPosition);

        dialoguePanel.gameObject.SetActive(false);

        OnDialogueFinished?.Invoke();
    }

    // =========================================================
    // PANEL SLIDE
    // =========================================================

    IEnumerator SlidePanel(Vector2 target)
    {
        while (Vector2.Distance(dialoguePanel.anchoredPosition, target) > 0.5f)
        {
            dialoguePanel.anchoredPosition = Vector2.Lerp(
                dialoguePanel.anchoredPosition,
                target,
                slideSpeed * Time.deltaTime);

            yield return null;
        }

        dialoguePanel.anchoredPosition = target;
    }
}