using System;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int bounceTime = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindFirstObjectByType<WeaponsController>().GetAmmo();
            
            Destroy(gameObject);
        }
    }



    void Update()
    {
        transform.Rotate(0f, 0f, 90f * Time.deltaTime);
        
    }
}
