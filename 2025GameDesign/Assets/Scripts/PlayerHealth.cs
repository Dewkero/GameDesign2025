using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth;

    public Image healthBarFill;
    public GameObject healthBar;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking dmg:" + damage + "Current health" + currentHealth);

        currentHealth -= damage;

        healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Current health after dmg:" + currentHealth);
        healthBar.SetActive(true);

    }

    void Die()
    {
        // Handle player death
        Debug.Log("Player Died!");
        Destroy(Player.instance);
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        
    }
}
