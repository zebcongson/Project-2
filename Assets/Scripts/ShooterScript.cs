using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{

    public GameObject projectile;
    public float timeToReload;
    public float fireSpeed;
    Camera cam;

    float reloadTimer = 0;

    

    void Awake()
    {
        cam = Camera.main;
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.J) && reloadTimer < 0)
        {
            Attack();
            reloadTimer = timeToReload;
        }
        else
        {
            reloadTimer -= Time.deltaTime;
        }

       
    }
    void Attack()
    {
       
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;

        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle - 90f);

        GameObject p = Instantiate(projectile, transform.position, rot);
        p.GetComponent<Rigidbody2D>().linearVelocity = dir * fireSpeed;
        Destroy(p, 3f);
    }

}
