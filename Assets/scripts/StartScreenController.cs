using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    [Header("UI")]
    public Image blueScreen;
    public TMP_Text titleText;
    public TMP_Text pressEnterText;

    [Header("Settings")]
    public Color blueColor = new Color(0.05f, 0.25f, 1f, 1f);
    public Color redFlash = new Color(0.45f, 0.02f, 0.02f, 1f);
    
    public Player2DMovement playerMovement;
    public playersit playerSit;

    private bool gameStarted = false;

    private string normalTitle = "TESTGAME1.EXE";

    private string[] glitches =
    {
    "TESTGAM_1.EXE",
    "TESTG_ME1.EXE",
    "TE_TGAME1.EXE",
    "TESTGAME1.E_E",
    "_ESTGAME1.EXE",
    "TES__AME1.EXE",
    "TESTGAMEI.EXE",    
    "TESTGAMEL.EXE",    
    "TESTGAME7.EXE",
    "TESTGAME?.EXE",
    "TESTGAME1.EX_",
    "TESTGAME1.EX3",
    "TESTGAMEI.EX_",
    "TESTGAME!.EXE",
    "TESTGAME1.EX",
    "TESTGAME1.EXX",
    "TESTG4ME1.EXE",
    "TESTGAMEl.EXE",    
    "TEST6AME1.EXE",
    "TESTGAMEO.EXE",    
    "TESTGAM31.EXE",
    "TES7GAME1.EXE",
    "TESTGAME_.EXE",
    };

    private int glitchCounter = 0;

    void Start()
{
    blueScreen.color = blueColor;

    playerMovement.enabled = false;

    StartCoroutine(BlinkPressEnter());
    StartCoroutine(TitleLoop());
}
    void Update()
{
    if (gameStarted)
        return;

    if (playerSit.isSitting && Input.GetKeyDown(KeyCode.Return))
    {
        gameStarted = true;

        StopAllCoroutines();

        pressEnterText.enabled = false;

        titleText.enabled = false;

        blueScreen.enabled = false;

        playerMovement.enabled = true;

        // TODO: Start Game BGM

        Debug.Log("Game Started");
    }
}
    //------------------------------------------------
    // PRESS ENTER BLINK
    //------------------------------------------------

    IEnumerator BlinkPressEnter()
    {
        while (true)
        {
            pressEnterText.enabled = !pressEnterText.enabled;

            yield return new WaitForSeconds(Random.Range(0.45f, 0.8f));
        }
    }

    //------------------------------------------------
    // TITLE LOOP
    //------------------------------------------------

    IEnumerator TitleLoop()
    {
        while (true)
        {
            // Wait before another glitch
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

            // Pick random glitch text
            titleText.text = glitches[Random.Range(0, glitches.Length)];

            // Leave it visible long enough to notice
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            glitchCounter++;

            if (glitchCounter >= 4)
            {
                yield return StartCoroutine(BigRedFlash());
                glitchCounter = 0;
            }
            else
            {
                yield return StartCoroutine(SmallBlueFlick());
            }

            // Restore title
            titleText.text = normalTitle;
        }
    }

    //------------------------------------------------
    // SMALL BLUE FLICK
    //------------------------------------------------

    IEnumerator SmallBlueFlick()
    {
        blueScreen.color = new Color(
            blueColor.r * 0.7f,
            blueColor.g * 0.7f,
            blueColor.b * 0.7f,
            1f);

        yield return new WaitForSeconds(0.05f);

        blueScreen.color = blueColor;
    }

    //------------------------------------------------
    // BIG RED FLASH
    //------------------------------------------------

    IEnumerator BigRedFlash()
    {
        blueScreen.color = redFlash;

        yield return new WaitForSeconds(0.04f);

        blueScreen.color = blueColor;

        yield return new WaitForSeconds(0.04f);

        blueScreen.color = redFlash;

        yield return new WaitForSeconds(0.03f);

        blueScreen.color = blueColor;
    }
}