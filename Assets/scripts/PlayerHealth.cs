using UnityEngine;

public class PlayerHealth : MonoBehaviour

{
    public int health = 100;
    public DeathScreenController deathScreen;
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            deathScreen.Die();
        }
    }
}