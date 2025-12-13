using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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

    void Start()
    {
        playerScript = GetComponent<Player>();
        playerHealth = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //update cooldown timer
        if (!canHide)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canHide = true;
                Debug.Log("Cooldown ended. You can now hide again.");
            }
        }

        //update hide timer
        if (isHiding)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
            {
                ExitHide();
            }
        }

        if (hidingSpot != null && canHide)
        {
            if (Input.GetKeyDown(KeyCode.W) && !isHiding && isInHidingSpot) // Press W to hide
            {
                Debug.Log("W pressed inside hide Spot");
                EnterHide();
            }

            if (Input.GetKeyDown(KeyCode.S) && isHiding) // Press S to exit hide
            {
                Debug.Log("S pressed. Exiting hide.");
                ExitHide();
            }
        }

    }

    private void EnterHide()
    {
        isHiding = true;
        hideTimer = hideDuration;
        Debug.Log("EnterHide called" + isHiding);
        cooldownTimer = hideCooldown;

        if (!isInHidingSpot)
        {
            Debug.LogWarning("Trying to hide, not in spot");
            return;
        }

        //disable player dmg
        if (playerHealth != null)
        {
            playerHealth.enabled = false;
            Debug.Log("PlayerHealth disabled while hiding.");
        }

        //dsiable collider
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            Debug.Log("Player collider disabled while hiding.");
        }

        if (playerRB != null)
        {
            playerRB.velocity = Vector2.zero;
            playerRB.isKinematic = true;
            Debug.Log("PLayer RB diabled");
        }

        //disable chase
        Ghoul[] ghouls = FindObjectsOfType<Ghoul>();
        foreach (Ghoul g in ghouls)
        {
            g.isChasing = false;
        }

        //hide player sprite
        if (playerScript.spriteRenderer != null)
        {
            playerScript.spriteRenderer.enabled = false;
            Debug.Log("Player spriteRenderer disabled while hiding.");
        }
    }

    private void ExitHide()
    {
        isHiding = false;
        canHide = false;
        cooldownTimer = hideCooldown;

        //enable player dmg
        if (playerHealth != null)
        {
            playerHealth.enabled = true;
            Debug.Log("PlayerHealth enabled after exiting hide.");
        }

        //enable move
        if (playerRB != null)
        {
            playerRB.isKinematic = false;
            Debug.Log("Player RB enabled");
        }

        //enable collider
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
            Debug.Log("Player collider enabled after exiting hide.");
        }

        //unhide player sprite
        if (playerScript.spriteRenderer != null)
        {
            playerScript.spriteRenderer.enabled = true;
            Debug.Log("Player spriteRenderer enabled after exiting hide.");
        }
    }

    //Detect hide spots
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            hidingSpot = collision;
            isInHidingSpot = true;
            Debug.Log("Entered hiding spot: " + collision.name + " | isInHidingSpot = " + isInHidingSpot);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            hidingSpot = null;
            isInHidingSpot = false;
            Debug.Log("Exited hiding spot: " + collision.name + " | isInHidingSpot = " + isInHidingSpot);
            
            if (isHiding)
            {
                ExitHide(); // Exit hiding if the player leaves the hide spot
            }
        }

    }
}