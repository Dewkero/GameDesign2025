using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform gunPoint;

    public float shootTime;
    public float shootCounter;

    public Animator anim;
    public Player player;

    private Vector3 originalLocalScale;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        if (player == null)
        {
            Debug.LogError("Can't find Player script in parent");
        }

        originalLocalScale = transform.localScale;

        shootCounter = shootTime;
    }
    // Update is called once per frame
    void Update()
    {
        //skip shoot if hide
        if (player != null && player.GetComponent<Hide>() != null && player.GetComponent<Hide>().isHiding)
        {
            return;
        }

        if (shootCounter > 0f)
        {
            shootCounter -= Time.deltaTime;
        }

        Vector3 scale = originalLocalScale;
        scale.x = player.facingRight ? Mathf.Abs(originalLocalScale.x) : -Mathf.Abs(originalLocalScale.x);
        transform.localScale = scale;

        if (Input.GetButtonDown("Fire1") && shootCounter <= 0)
        {
           Shoot();
            shootCounter = shootTime;
        }
    }

    void Shoot ()
    {
        GameObject bullet = Instantiate(projectilePrefab, gunPoint.position, Quaternion.identity);

        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.facingRight = player.facingRight;
        }

        if (anim != null)
        {
            anim.SetTrigger("shoot");
        }
    }

}
