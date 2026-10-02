using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float existingTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        existingTime -= Time.deltaTime;
        if(existingTime < 0)
        {
            Destroy(gameObject);
        }
    }
    public float damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bounds"))
            Destroy(gameObject);
    }
}
