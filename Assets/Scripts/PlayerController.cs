using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed = 3;
    float x_input;
    float y_input;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Health_variables
    public float maxHealth = 5;
    public float currHealth = 5;
    public Slider HPSlider;
    #endregion


    #region Animation_components
    Animator anim;
    #endregion

    #region Unity_functions
    private void Awake() {
        /* TODO: Update your Awake function to initialize all variables needed. This includes your attackTimer, and your HPSlider.value.*/
        attackTimer = 0;
        /* TODO 4.1: Set HPSlider.value to a ratio between the 
            player's current health and maximum health. */
        HPSlider.value = currHealth / maxHealth;
      
        PlayerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if (isAttacking)
        {
            return;
        }
        /*TODO 1.1: Write an Update function that will call the Move() helper function while also updating the x_input and y_input values.
        You will also need to edit this function when you call attacks, and interacting with chests.*/
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        Move();
        /* TODO 1.2: Check if the attack key is being pressed. If so, attack by calling your Attack() function
         * IMPORTANT:  You will need to use `Input.GetKeyDown(KeyCode key)` to determine if the key is being pressed
        */
        if(Input.GetKeyDown(KeyCode.J) && attackTimer < 0)
        {
            Attack();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        /* TODO 1.3: Modify your attack conditional statement to only attack when attackTimer < 0. Otherwise, decrement the attackTimer. */
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
    #endregion



    #region Attack_variables
    public float damage = 2;
    public float attackSpeed = 1;
    float attackTimer;
    public float hitboxTiming = 0.1f;
    public float endAnimationTiming = 0.1f;
    bool isAttacking;
    Vector2 currDirection;
    #endregion

    #region Attack_functions
    private void Attack()
    {
        // TODO 1.3: Set the attackTimer to attackSpeed to reset the attack cooldown 
        Debug.Log("Attacking now");
        Debug.Log(currDirection);
        attackTimer = attackSpeed;
        StartCoroutine(AttackRoutine());
        

    }

    IEnumerator AttackRoutine()
    {
        /* Note: You will need to edit this function in the animation section, the enemy section, and in the sound section.*/
        isAttacking = true;
        PlayerRB.linearVelocity = Vector2.zero;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(hitboxTiming);
        Debug.Log("Casting hitbox now");
        FindFirstObjectByType<AudioManager>().Play("PlayerAttack");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if(hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Tons of Damage");
                /* TODO 3.2: Call TakeDamage() inside of the enemy's Enemy script using
                the "hit" reference variable */

                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
                FindFirstObjectByType<AudioManager>().Play("EnemyHurt");
            }
        }

        yield return new WaitForSeconds(hitboxTiming);
        isAttacking = false;
    }
    
    #endregion

    #region Movement_functions
    private void Move()
    {
        /*TODO 1.1: Edit the Move() function which will set PlayerRB.velocity to a vector based on which input the player is pressing.*/
        if(y_input>0)
        {
            PlayerRB.linearVelocity = Vector2.up * moveSpeed;
            currDirection = Vector2.up;
        }
        else if(y_input<0)
        {
            PlayerRB.linearVelocity = Vector2.down * moveSpeed;
            currDirection = Vector2.down;
        }
        else if (x_input > 0)
        {
            PlayerRB.linearVelocity = Vector2.right * moveSpeed;
            currDirection = Vector2.right;
        }
        else if (x_input < 0)
        {
            PlayerRB.linearVelocity = Vector2.left * moveSpeed;
            currDirection = Vector2.left;
        }
        else
        {
            PlayerRB.linearVelocity = Vector2.zero;
        }
        /* TODO 1.4: Set currDirection to the correct Vector direction i.e. Vector2.left.
         * HINT: there are four cardinal directions. */

        /* DO NOT MODIFY ANYTHING BELOW THIS LINE UNLESS YOU REALLY KNOW WHAT YOU'RE DOING */

        if (x_input == 0 && y_input == 0)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);

        }

        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);

    }
    #endregion

    #region Health_functions

    public void TakeDamage(float value)
    {
        /* TODO 3.1: Adjust currHealth when the player takes damage
        IMPORTANT: What happens when the player's health reaches 0? */
        currHealth -= value;
        HPSlider.value = currHealth / maxHealth;
        if (currHealth <= 0)
        {
            Die();
        }
        /* TODO 4.1: Update the value of HPSlider after the player's health changes. */
        
        FindFirstObjectByType<AudioManager>().Play("PlayerHurt");
    }

    public void Heal(float value)
    {
        /* TODO 3.1: Adjust currHealth when the player heals
        IMPORTANT: What happens when the player's health surpasses their max health? Should currHealth be above maxHealth?*/
        if (currHealth + value >= maxHealth)
        {
            currHealth = maxHealth;
        }
        else
        {
            currHealth += value;
        }
        /* TODO 4.1: Update the value of HPSlider after the player's health changes. */
        HPSlider.value = currHealth / maxHealth;
    }

    public void Die()
    {
        FindFirstObjectByType<AudioManager>().Play("PlayerDeath");
        
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().LoseGame();
        Destroy(this.gameObject);
    }
    #endregion

    #region Interact_functions
    private void Interact()
    {
        /* TODO 6.3: Use a BoxCastAll raycast to check what is infront of the player. 
         * If there is a chest game object, open the chest by calling it's Open() function */
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, new Vector2(0.5f,0.5f),0f, Vector2.zero,0f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Chest"))
            {
                Debug.Log("Opened chest");
                hit.transform.GetComponent<Chest>().Open();
                
            }
        }

    }
    #endregion
}
