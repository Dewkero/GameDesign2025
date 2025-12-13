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
            Hide hideScript = collision.gameObject.GetComponent<Hide>();
            if (hideScript != null && hideScript.isHiding)
            {
                return;
            }

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
