using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int amount)
    {   
        health = Mathf.Max(0, health - amount);
        if (health <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}