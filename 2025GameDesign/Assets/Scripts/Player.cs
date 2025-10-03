using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;

    public bool flippedleft;
    public bool facingRight;

    // hiding variables
    public bool canHide = false;
    public bool hiding = false;

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

    void Flip (bool facingRight)
    {

    }


    //hiding
    private void LateUpdate()
    {
        if(canHide && Input.GetKey(KeyCode.W))
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            hiding = true; 
        }
        else
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
            hiding = false;
        }

    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);


        // hiding
        if (!hiding)
        {
            playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = false;
        }
    }

}
