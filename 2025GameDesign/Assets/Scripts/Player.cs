using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Rigidbody2D playerRB;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;

    public Animator anim;

    public bool flippedLeft;
    public bool facingRight = true;

    private Hide hideScript;

    public AudioSource footstepSound;
    public AudioClip footstepClip;

    private bool isMoving = false;
    private bool isWalkingSoundPlaying = false;

    private void Start()
    {
        hideScript = GetComponent<Hide>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        if (moving && !isWalkingSoundPlaying) 
        { 
            if (footstepSound != null && footstepClip != null)
            {
                footstepSound.clip = footstepClip;
                footstepSound.loop = true;
                footstepSound.Play();
                isWalkingSoundPlaying = true;
            }
        }

        else if (!moving && isWalkingSoundPlaying) 
        { 
            if (footstepSound != null)
            {
                footstepSound.Stop();
                isWalkingSoundPlaying = false;
            }
        }

        if (hideScript != null && hideScript.isHiding)
        {
            anim.SetFloat("horizontal", 0);
            return; // Skip movement and animation updates while hiding
        }

        input = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("horizontal", Mathf.Abs(input));

        // flips sprite based on direction, import later
        if (input < 0)
        {
            facingRight = false;
        }
        else if (input > 0)
        {
            facingRight = true;
        }

        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        if (hideScript != null && hideScript.isHiding)
        {
            playerRB.velocity = Vector2.zero;
            return;
        }

        playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
    }
}
