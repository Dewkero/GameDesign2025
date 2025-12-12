using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulDamage : MonoBehaviour
{

    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ghoul collided with Player.");

            Hide hidePlayer = collision.gameObject.GetComponent<Hide>();
            if (hidePlayer != null && hidePlayer.isHiding)
            {
                Debug.Log("Player is hiding, no damage taken.");
                return;
            }

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.enabled)
            {
                Debug.Log("Player takes dmg");
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.Log("PlayerHealth component not found or disabled.");
            }
        }
    }
}
