using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepyPack : MonoBehaviour
{
    

    #region Heal_functions
    private void OnTriggerEnter2D(Collider2D other)
    {
        /* TODO Part 6.1: If this object collides with the player, heal the player by healAmount by calling the player's Heal() function.
         * After healing this health potion should be destroyed.
         * HINT: The variable, other, contains a reference to the object that collides with this health potion. */
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    #endregion
}

