using UnityEngine;

public class DoorTrigger2 : MonoBehaviour
{
    
    private bool playerNearby;
    public Transform room2Spawn;
    public Transform dungeonRoot;
    public Transform player;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            dungeonRoot.position += new Vector3(0, 12f, 0);
        }
        player.position = room2Spawn.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            playerNearby = true;
            PromptManager.Instance.ShowE();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Invoke(nameof(HidePrompt), 0.02f);       }
    }
    void HidePrompt()
{
    PromptManager.Instance.Hide();
}
}