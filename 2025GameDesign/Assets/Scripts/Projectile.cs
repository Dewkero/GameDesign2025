using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D projectileRB;
    public float speed;

    public float projectileTime;
    public float projectileCounter;

    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        projectileCounter = projectileTime;
     
        if (!facingRight)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        projectileRB.velocity = new Vector2(facingRight ? speed : -speed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        projectileCounter -= Time.deltaTime;
        if(projectileCounter <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ghoul")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
