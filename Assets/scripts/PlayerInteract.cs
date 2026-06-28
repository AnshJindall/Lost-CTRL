using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 5f;
    public playersit playerSit;
    public KeypadInteract keypadInteract;
    public SafeInteract safeInteract;

    void Update()
    {
        // 1. Unsit Logic (ESC Key)
        if (Input.GetKeyDown(KeyCode.Escape)
    && !keypadInteract.keypadUI.activeSelf
    && !safeInteract.safeUI.activeSelf)
            {
                if (playerSit.IsSitting())
                {
                    playerSit.ToggleSit();
                }
            }
        // 2. Interact Logic (E Key)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Only allow interaction if the player is NOT sitting
            if (!playerSit.IsSitting())
            {
                TryInteract();
            }
        }
    }

    void TryInteract()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        int mask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, out hit, interactDistance, mask))
        {
            Debug.Log("Hit: " + hit.collider.name);
            hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }
}