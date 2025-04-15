using Unity.Mathematics;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 10f, damageAmount = 15f;

    public Rigidbody rb;
    
    public GameObject impactEffect, damageEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
