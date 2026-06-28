using UnityEngine;
using System.Collections;

public class EnemyController2 : MonoBehaviour
{
    public float speed = 2f;

    private int direction = 1;
    private bool canTurn = true;

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
{

    if (other.CompareTag("TurnPoint") && canTurn)
    {
        direction *= -1;
        StartCoroutine(TurnCooldown());
    }
}

    private IEnumerator TurnCooldown()
    {
        canTurn = false;
        yield return new WaitForSeconds(0.2f);
        canTurn = true;
    }
}