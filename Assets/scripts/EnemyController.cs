using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;

    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector2.up * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TurnPoint"))
        {
            direction *= -1;
        }
    }
}