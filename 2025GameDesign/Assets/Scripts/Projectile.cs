using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D projectileRB;
    public float speed;

    public float projectileTime;
    public float projectileCounter;

    // Start is called before the first frame update
    void Start()
    {
        projectileCounter = projectileTime;
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

    private void FixedUpdate()
    {
        projectileRB.velocity = new Vector2(speed, projectileRB.velocity.y);
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
