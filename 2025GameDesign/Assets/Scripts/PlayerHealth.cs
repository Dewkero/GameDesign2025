using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public Image healthBarFill;

    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.SetActive(true);
    }
}
