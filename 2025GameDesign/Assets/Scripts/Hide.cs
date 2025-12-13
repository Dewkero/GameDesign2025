using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hide : MonoBehaviour
{
    public float hideDuration = 10f;
    public float hideCooldown = 10f;

    private bool canHide = true;
    public bool isHiding = false;
    private float hideTimer = 0f;
    private float cooldownTimer = 0f;

    private Player playerScript;
    private Collider2D hidingSpot;
    private PlayerHealth playerHealth;
    private Collider2D playerCollider;
    private Rigidbody2D playerRB;

    private bool isInHidingSpot = false;

    public TMP_Text feedbackText;
    private float feedbackDuration = 2f;
    private float feedbackTimer = 0f;

    void Start()
    {
        playerScript = GetComponent<Player>();
        playerHealth = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Update cooldown timer
        if (!canHide)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canHide = true;
                ShowFeedback("You feel your breath return.");
            }
        }

        // Update hide timer
        if (isHiding)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
            {
                ExitHide();
                ShowFeedback("You feel yourself run out of breath.");
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && !isHiding && isInHidingSpot && canHide) // Press W to hide
        {
            Debug.Log("W pressed inside hide spot.");
            EnterHide();
        }

        if (Input.GetKeyDown(KeyCode.S) && isHiding)
        {
            ExitHide();
        }

        if (feedbackText != null && feedbackText.gameObject.activeSelf)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0f)
            {
                StartCoroutine(FadeOutText());

            }
        }

    }

    private void EnterHide()
    {
        if (!isInHidingSpot) return;

        isHiding = true;
        hideTimer = hideDuration;
        cooldownTimer = hideCooldown;
        canHide = false;

        gameObject.layer = LayerMask.NameToLayer("Hidden Player");

        if (playerRB != null)
        {
            playerRB.velocity = Vector2.zero;
            Debug.Log("Player Rigidbody2D disabled.");
        }

        // Disable sprite renderer
        if (playerScript.spriteRenderer != null)
        {
            playerScript.spriteRenderer.enabled = false;
            Debug.Log("Player spriteRenderer disabled while hiding.");
        }

        // Disable enemy chase behavior (assuming you have a Ghoul script)
        Ghoul[] ghouls = FindObjectsOfType<Ghoul>();
        foreach (Ghoul g in ghouls)
        {
            g.isChasing = false;
        }

        //hide health bar
        if (playerHealth != null && playerHealth.healthBar != null)
        {
            playerHealth.healthBarFill.gameObject.SetActive(false);
            playerHealth.healthBar.SetActive(false);
        }

    }

    private void ExitHide()
    {
        isHiding = false;

        gameObject.layer = LayerMask.NameToLayer("Player");

        // Enable sprite renderer
        if (playerScript.spriteRenderer != null)
        {
            playerScript.spriteRenderer.enabled = true;
            Debug.Log("Player spriteRenderer enabled after exiting hide.");
        }

        // Reset cooldown timer
        cooldownTimer = hideCooldown;
        canHide = false;

        //show health bar
        if (playerHealth != null && playerHealth.healthBar != null)
        {
            playerHealth.healthBar.SetActive(true);
            playerHealth.healthBarFill.gameObject.SetActive(true);
        }
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            feedbackText.alpha = 0f;

            StartCoroutine(FadeInText());
            feedbackTimer = feedbackDuration;
        }
    }
    private IEnumerator FadeInText()
    {
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        // fade in
        while (elapsedTime < fadeDuration)
        {
            feedbackText.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        feedbackText.alpha = 1f; // 100%

        yield return new WaitForSeconds(feedbackDuration); 

        StartCoroutine(FadeOutText()); // fade out
    }

    private IEnumerator FadeOutText()
    {
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        // fade out
        while (elapsedTime < fadeDuration)
        {
            feedbackText.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        feedbackText.alpha = 0f; // Ensure fully transparent
        feedbackText.gameObject.SetActive(false); // Hide it once fully faded out
    }

    // Detect if the player enters a hiding spot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            hidingSpot = collision;
            isInHidingSpot = true;
            Debug.Log("Entered hiding spot: " + collision.name);
        }
    }

    // Detect if the player exits a hiding spot
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            hidingSpot = null;
            isInHidingSpot = false;
            Debug.Log("Exited hiding spot: " + collision.name);

            if (isHiding)
            {
                ExitHide();
            }
        }
    }
}

