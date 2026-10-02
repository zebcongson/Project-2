using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.Cinemachine.SplineAutoDolly;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed = 3;
    float x_input;
    float y_input;
    Vector2 currDirection;
    
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Health_variables
    public float maxHealth = 5;
    public float currHealth = 5;
    #endregion


    #region Animation_components
    Animator anim;
    #endregion

    #region Unity_functions
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }
    private void Update()
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            x_input = -1;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            x_input = 1;
        else
        {
            x_input = 0;
        }

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            y_input = -1;
        else if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            y_input = 1;
        else
        {
            y_input = 0;
        }
        Move();
        if (Keyboard.current.jKey.isPressed && reloadTimer < 0)
        {
            Attack();
            reloadTimer = timeToReload;
        }
        else
        {
            reloadTimer -= Time.deltaTime;
        }
        if (x_input == 0 && y_input == 0)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }


    }
    #endregion
    #region Movement_functions
    private void Move()
    {
        Vector2 inputDir = new Vector2(x_input, y_input).normalized;
        PlayerRB.linearVelocity = inputDir * moveSpeed;

        if (inputDir != Vector2.zero)
        {
            currDirection = inputDir;
        }

        if (x_input == 0 && y_input == 0)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }
    }
        /* TODO 1.4: Set currDirection to the correct Vector direction i.e. Vector2.left.
         * HINT: there are four cardinal directions. */

        /* DO NOT MODIFY ANYTHING BELOW THIS LINE UNLESS YOU REALLY KNOW WHAT YOU'RE DOING */

        

        

    
    #endregion
    #region Attack_variables
    public GameObject projectile;

    public float damage = 2;
    public float timeToReload;
    public float fireSpeed;
    Camera cam;
    float reloadTimer = 0;
    #endregion

    #region Attack_function
    void Attack()
    {

        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);
        Vector2 dir = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle - 90f);

        GameObject p = Instantiate(projectile, transform.position, rot);
        p.GetComponent<Projectile>().damage = damage;
        p.GetComponent<Rigidbody2D>().linearVelocity = dir * fireSpeed;
        Destroy(p, 3f);
    }

    #endregion

    #region Health_functions

    public void TakeDamage(float value)
    {
        
        currHealth -= value;
        if (currHealth <= 0)
        {
            Die();
        }
        

        
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();

            if (enemyScript != null && enemyRB != null && enemyScript.timeBeforeAttack <= 0)
            {
                Vector2 bounceDirection = (transform.position - other.transform.position).normalized;

                enemyRB.linearVelocity = -bounceDirection * enemyScript.bounceForce;
                enemyScript.timeBeforeAttack = enemyScript.bounceCooldown;

                TakeDamage(enemyScript.Damage);
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}
