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

    // Update is called once per frame
    void Update()
    {
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
        playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
    }

    //void Flip (bool facingRight)
    //{
    //    if(flippedLeft && facingRight)
    //    {
    //        transform.Rotate(0f, 180f, 0f);
    //        flippedLeft = false;
    //    }

    //    if (!flippedLeft && !facingRight)
    //    {
    //        transform.Rotate(0f, -180f, 0f);
    //        flippedLeft = true;
    //    }
    //}
}
