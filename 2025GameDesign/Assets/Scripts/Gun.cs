using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform gunPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //what will spawn, where will spawn, rotation of spawn
            Instantiate(projectilePrefab, gunPoint.position, Quaternion.identity);
        }
    }
}
