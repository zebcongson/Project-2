using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed;
    #endregion

    #region Targeting_variables
    public Transform player;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region Attack_variables
    public float Damage;
    public float DamageRadius;
    #endregion

    #region Physics_components
    Rigidbody2D EnemyRB;
    #endregion

    #region Unity_functions

    private void Awake()
    {
        currHealth = maxHealth;
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /* TODO 2.1: Call Move() if player is !null */
        if (player == null)
        {
            return;
        }
        Move();
    }
    #endregion

    #region Movement_functions
    private void Move()
    {
        /* TODO 2.1: Move the enemy towards the player */
        Vector2 direction = player.position - transform.position;
        EnemyRB.linearVelocity = direction.normalized * moveSpeed;
    }
    #endregion

}