using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPotion : MonoBehaviour
{
    #region HealthPotion_variables
    [SerializeField]
    [Tooltip("amount the player takes damage")]
    private int dmgAmount;
    #endregion

    #region Heal_functions
    private void OnTriggerEnter2D(Collider2D other)
    {
        /* TODO Part 6.1: If this object collides with the player, heal the player by healAmount by calling the player's Heal() function.
         * After healing this health potion should be destroyed.
         * HINT: The variable, other, contains a reference to the object that collides with this health potion. */
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().TakeDamage(dmgAmount);
            FindFirstObjectByType<AudioManager>().Play("Potion");
            Debug.Log("hurt to " + other.transform.GetComponent<PlayerController>().currHealth);
            Destroy(gameObject);
        }

    }
    #endregion
}

