using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardController : MonoBehaviour
{
    [Header("References")]
    public Transform importantKeys;

    [Header("Shake")]
    public float keyboardShakeAmount = 0.01f;
    public float keyboardShakeSpeed = 40f;

    [Header("Key Vibration")]
    public float keyVibrationAmount = 0.002f;

    [Header("Lift")]
    public float liftHeight = 0.02f;
    public float liftSpeed = 2f;

    private Vector3 keyboardStartPos;

    private List<Transform> keys = new();
    private Dictionary<Transform, Vector3> originalPositions = new();

    void Start()
    {
        keyboardStartPos = transform.localPosition;

        foreach (Transform key in importantKeys)
        {
            keys.Add(key);
            originalPositions[key] = key.localPosition;
        }
    }

    public void BeginPossession()
    {
        StartCoroutine(PossessionSequence());
    }

    IEnumerator PossessionSequence()
    {
        // Shake keyboard immediately
        StartCoroutine(ShakeKeyboard());

        // Tiny pause
        yield return new WaitForSeconds(0.5f);

        // Lift + vibrate keys
        StartCoroutine(FloatKeys());
    }

    IEnumerator ShakeKeyboard()
    {
        while (true)
        {
            Vector2 random =
                Random.insideUnitCircle * keyboardShakeAmount;

            transform.localPosition =
                keyboardStartPos +
                new Vector3(random.x, random.y, 0);

            yield return null;
        }
    }

    IEnumerator FloatKeys()
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;

            foreach (Transform key in keys)
            {
                Vector3 start = originalPositions[key];

                float lift =
                    Mathf.Lerp(
                        0,
                        liftHeight,
                        timer * liftSpeed);

                Vector2 jitter =
                    Random.insideUnitCircle *
                    keyVibrationAmount;

                key.localPosition =
                    start +
                    new Vector3(
                        jitter.x,
                        lift,
                        jitter.y);
            }

            yield return null;
        }
    }
}