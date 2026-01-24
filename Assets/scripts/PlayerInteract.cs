using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
     public float interactDistance = 5f;
    public playersit playerSit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If sitting, E = stand up
            if (playerSit.IsSitting())
            {
                playerSit.ToggleSit();
                return;
            }

            // Otherwise, interact normally
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        int mask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, out hit, interactDistance, mask))
        {
            hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }
    }
