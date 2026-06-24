using UnityEngine;

public class drawerinteract : MonoBehaviour
{
    public float openDistance = 0.3f;
    public float speed = 5f;
    public Vector3 openDirection = new Vector3(1, 0, 0); // tweak in inspector

    [Header("Hint System")]
    public bool goldenKeypadHint = false;
    
    // Changed to a softer, warmer pale gold (RGB)
    public Color glowColor = new Color(1f, 0.9f, 0.6f); 
    
    // Drastically lowered from 1.5f to a subtle 0.3f
    public float maxGlowIntensity = 0.3f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;
    private bool isMoving = false;
    private MeshRenderer drawerRenderer;

    void Start()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + openDirection.normalized * openDistance;
        drawerRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        HandleGlowHint();

        if (!isMoving) return;

        Vector3 target = isOpen ? openPos : closedPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * speed);

        if (Vector3.Distance(transform.localPosition, target) < 0.01f)
        {
            transform.localPosition = target;
            isMoving = false;
        }
    }

    void HandleGlowHint()
    {
        if (drawerRenderer == null) return;

        if (goldenKeypadHint)
        {
            // Creates a smooth pulsing effect between 0 and maxGlowIntensity
            float pulse = Mathf.PingPong(Time.time, maxGlowIntensity);
            
            // Enable emission and apply the color
            drawerRenderer.material.EnableKeyword("_EMISSION");
            drawerRenderer.material.SetColor("_EmissionColor", glowColor * pulse);
        }
        else
        {
            // Turn off the glow when the hint is false
            drawerRenderer.material.DisableKeyword("_EMISSION");
        }
    }

    public void Interact()
    {
        Debug.Log("DRAWER INTERACTED");

        if (isMoving) return;

        isOpen = !isOpen;
        isMoving = true;

        // Automatically turn off the hint once the player successfully opens the drawer
        if (isOpen && goldenKeypadHint)
        {
            goldenKeypadHint = false;
        }
    }
}