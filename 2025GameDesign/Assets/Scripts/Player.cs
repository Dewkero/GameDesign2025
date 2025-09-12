using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Rigidbody rb;
    //private SpriteRenderer rend;

    public float maxXPosition;
    public float horizontalMovementSpeed;
    //private float dirX;

    public KeyCode leftKey;
    public KeyCode rightKey;
    //public KeyCode upKey;

   // private bool canHide = false;
   // private bool Hiding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey) && transform.position.x < maxXPosition)
        {
            MoveLeft();
        }
        else if (Input.GetKey(rightKey) && transform.position.x > -maxXPosition)
        {
            MoveRight();
        }

        void MoveLeft()
        {
            transform.position += Vector3.left * horizontalMovementSpeed;
        }

        void MoveRight()
        {
            transform.position += Vector3.right * horizontalMovementSpeed;
        }

        //void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.name.Equals("HideSpot"))
        //    {
        //        canHide = true;
        //    }
        //}

        //void OnTriggerExit2D(Collider2D other)
        //{
        //    if (other.gameObject.name.Equals("HideSpot"))
        //    {
        //        canHide = false;
        //    }
        //}
    }
}
