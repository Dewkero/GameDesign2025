using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Ghoul : MonoBehaviour
{
    public float moveSpeed;

    public Transform playerTransform;
    public bool isChasing = false;
    public float chaseDistance;

    private float direction = 1f;
    private float changeDirectionTime = 0f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found");
        }

        GameObject[] ghouls = GameObject.FindGameObjectsWithTag("Ghoul");
        foreach (GameObject g in ghouls)
        {
            if (g != gameObject)
            {
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

        changeDirectionTime = Time.time + Random.Range(1f, 3f);
        direction = Random.value >0.5f ? 1f : -1f;
    }
    void Update()
    {
        if (playerTransform == null) return;

        if (!isChasing && Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }

        FlipSprite();

        void ChasePlayer()
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;
        }

        void Wander()
        {
            if (Time.time > changeDirectionTime)
            {
                direction = Random.value > 0.5f ? 1f : -1f;
                changeDirectionTime = Time.time + Random.Range(2f, 4f);
            }

            //left or right
            transform.position += new Vector3(direction, 0, 0) * moveSpeed * Time.deltaTime;
        }

        void FlipSprite()
        {
            if (direction > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1f;
        }
    }
}
