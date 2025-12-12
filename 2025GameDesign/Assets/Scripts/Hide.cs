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
            if (Input.GetKeyDown(KeyCode.W) && !isHiding)
            {
                EnterHide();
            }

            if (Input.GetKeyDown(KeyCode.S) && isHiding)
            {
                ExitHide();
            }
        }
    }

    private void EnterHide()
    {
        isHiding = true;
        hideTimer = hideDuration;

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
            Debug.Log("Entered hide spot");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            hidingSpot = null;
            if (isHiding)
            {
                ExitHide();
                Debug.Log("Exited hide spot");
            }
        }
    }

}
