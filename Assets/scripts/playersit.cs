using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersit : MonoBehaviour
{
    public Transform sitPoint;
    public float sitSpeed = 5f;

    public bool isSitting = false;
    private bool isMoving = false;

    private Vector3 standPos;
    private Quaternion standRot;

    void Start()
    {
        standPos = transform.position;
        standRot = transform.rotation;
    }

    void Update()
    {
        if (!isMoving) return;

        if (isSitting)
        {
            transform.position = Vector3.Lerp(transform.position, sitPoint.position, Time.deltaTime * sitSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, sitPoint.rotation, Time.deltaTime * sitSpeed);

            if (Vector3.Distance(transform.position, sitPoint.position) < 0.05f)
            {
                transform.position = sitPoint.position;
                transform.rotation = sitPoint.rotation;
                isMoving = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, standPos, Time.deltaTime * sitSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, standRot, Time.deltaTime * sitSpeed);

            if (Vector3.Distance(transform.position, standPos) < 0.05f)
            {
                transform.position = standPos;
                transform.rotation = standRot;
                isMoving = false;
            }
        }
    }

    public void ToggleSit()
    {
        if (isMoving) return;

        if (!isSitting)
        {
            // about to sit
            standPos = transform.position;
            standRot = transform.rotation;
        }

        isSitting = !isSitting;
        isMoving = true;
    }

    public bool IsSitting()
    {
        return isSitting || isMoving;
    }
}
