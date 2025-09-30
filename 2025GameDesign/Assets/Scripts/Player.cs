using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        // flips sprite based on direction, import later
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
    }
}
