using UnityEngine;

public class NoteInteract : MonoBehaviour
{
    public Transform holdPoint;
    private bool isHeld = false;
    private Rigidbody rb;
    private Collider noteCollider;

    [Header("Placement Settings")]
    // These will now actually control where the note goes
    public Vector3 holdOffset = new Vector3(0.335f, -0.201f, 0.4f); // Z set to 0.4 so it's in front of you
    public Vector3 holdRotation = new Vector3(177.6f, 10.2f, -89.95f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        noteCollider = GetComponent<Collider>();
    }

    public void Interact()
    {
        if (!isHeld) Pickup();
    }

    void Pickup()
    {
        isHeld = true;
        
        // 1. FIX SENSITIVITY LAG: Kill interpolation while holding
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.None; 

        // 2. PREVENT PHYSICS JITTER: Turn off the collider
        if (noteCollider != null) noteCollider.enabled = false;

        // 3. ATTACH TO CAMERA
        transform.SetParent(holdPoint);
        
        // 4. FIX TRANSFORM: Use the variables instead of Vector3.zero
        transform.localPosition = holdOffset; 
        transform.localEulerAngles = holdRotation; 
    }

    void Update()
    {
        if (isHeld)
        {
            // LIVE TWEAKING: This updates the position every frame while held
            // You can remove these two lines once you find your perfect values
            transform.localPosition = holdOffset; 
            transform.localEulerAngles = holdRotation; 

            // Dropping logic
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
        }
    }

    void Drop()
    {
        isHeld = false;
        
        // 1. Unparent it from the camera
        transform.SetParent(null);

        // 2. Safely move it away from your body BEFORE turning on colliders
        // Using Camera.main.transform.forward ensures it always drops where you look
        transform.position = holdPoint.position + Camera.main.transform.forward * 0.6f;

        // 3. Turn the physical body back on
        if (noteCollider != null) noteCollider.enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate; 

        // 4. Give it a tiny, gentle toss forward so it doesn't just drop straight down
        // (Make sure to reset velocity first so it doesn't carry momentum from you turning)
        rb.velocity = Vector3.zero; 
        rb.AddForce(Camera.main.transform.forward * 1.5f, ForceMode.Impulse);
    }
}