using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeypadManager : MonoBehaviour
{
    public TMP_Text displayText;

    private string currentCode = "";
    private bool puzzleSolved = false;

    public string correctCode = "1738";
    public Image displayFlash;
    public KeypadInteract keypadInteract;
    public SafeInteract safeInteract;

    void Update()
    {
        if (puzzleSolved)
        return;

    // Number keys
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                AddNumber(i.ToString());
            }
        }

        // Backspace
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DeleteLast();
        }

        // Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckCode();
        }
    }

    void AddNumber(string number)
    {
        if (currentCode.Length >= 4)
            return;

        currentCode += number;

        UpdateDisplay();
    }

    void DeleteLast()
    {
        if (currentCode.Length == 0)
            return;

        currentCode = currentCode.Substring(0, currentCode.Length - 1);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayText.text = "";

        foreach (char c in currentCode)
        {
            displayText.text += c + " ";
        }
    }

    void CheckCode()
{
    if (currentCode == correctCode)
    {
        StartCoroutine(Flash(Color.green, true));
    }
    else
    {
        StartCoroutine(Flash(Color.red, false));
    }
}       

    IEnumerator Flash(Color flashColor, bool correct)
{
    if (correct)
    {
        // Flash 1
        displayFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0.4f);
        yield return new WaitForSeconds(0.2f);

        displayFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        yield return new WaitForSeconds(0.15f);

        // Flash 2
        displayFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0.4f);
        yield return new WaitForSeconds(0.2f);

        displayFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        yield return new WaitForSeconds(0.2f);

        // Close the Keypad 
        puzzleSolved = true;
        currentCode = "";
        UpdateDisplay();
        keypadInteract.CloseKeypad();
        safeInteract.UnlockSafe();
    }
    else
    {
        // Single red flash
        displayFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0.4f);
        yield return new WaitForSeconds(0.35f);

        displayFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);

        currentCode = "";
        UpdateDisplay();
    }
}
}