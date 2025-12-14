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

    private Hide hideScript;

    public AudioSource dmgSound;
    public AudioClip dmgClip;

    // Start is called before the first frame update
    void Start()
    {
        hideScript = GetComponent<Hide>();

        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        if (dmgSound != null && dmgClip != null)
        {
            dmgSound.PlayOneShot(dmgClip);
        }

        if(hideScript != null && hideScript.isHiding)
        {
            return;
        }

        currentHealth -= damage;

        healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }

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
