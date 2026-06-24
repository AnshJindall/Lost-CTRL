using UnityEngine;

public class EnemyDamage2 : MonoBehaviour
{
    public Transform room2Spawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health =
                other.GetComponent<PlayerHealth>();

            health.TakeDamage(50);

            other.transform.position =
                room2Spawn.position;
        }
    }
}