using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    
    private bool playerNearby;
    public Transform room2Spawn;
    public Transform player;
    public GameObject tutorialText1;
    public GameObject tutorialText2;
    public GameObject enemy1;
    public Transform dungeonRoot;
    public GameObject visualise;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            dungeonRoot.position += new Vector3(0, -12f, 0);
            player.position = room2Spawn.position;
            if (tutorialText1 != null)
                tutorialText1.SetActive(false);

            if (tutorialText2 != null)
                tutorialText2.SetActive(true);

            if (enemy1 != null)
                enemy1.SetActive(true);

            if (visualise != null)
                visualise.SetActive(true);
        }
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
            Invoke(nameof(HidePrompt), 0.02f);     }
    }
    void HidePrompt()
{
    PromptManager.Instance.Hide();
}
}