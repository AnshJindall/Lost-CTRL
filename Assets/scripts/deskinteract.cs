using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deskinteract : MonoBehaviour
{
    public playersit playerSit;
    public void Interact()
    {
        playerSit.ToggleSit();
    }
}
