using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    #region GameObject_variables
    [SerializeField]
    private GameObject healthPotion;

    [SerializeField] private GameObject SleepyPotion;
    [SerializeField] private GameObject BadPotion;
    #endregion

    #region Chest_functions
    IEnumerator DestroyChest()
    {
        /* TODO Part 6.2: Instantiate the health potion at the chest's location and destroy the chest. */
        yield return null;
        int randInt = Random.Range(0, 100);
        if(randInt < 70)
        {
            Instantiate(healthPotion, transform.position, transform.rotation);
        }
        else if(randInt < 90)
        {
            Instantiate(SleepyPotion, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(BadPotion, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }

    public void Open()
    {
        StartCoroutine("DestroyChest");
    }
    #endregion
}
