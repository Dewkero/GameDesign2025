using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulDamage : MonoBehaviour
{

    public int damage;
    public PlayerHealth playerhealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerhealth.TakeDamage(damage);
        }
    }
}
