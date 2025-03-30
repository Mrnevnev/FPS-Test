using Unity.Mathematics;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public float range;

    public Transform cam;

    public LayerMask validLayers;

    public GameObject impactEffect, damageEffect;

    public GameObject muzzleFlare;

    public float flareDisplayTime;

    private float _flareCounter;

    public bool canAutoFire;

    public float timeBetweenShots;

    private float _shotCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_flareCounter > 0)
        {

            //Instantiate(muzzleFlash, Vector3.forward, quaternion.identity);
            _flareCounter -= Time.deltaTime;

            if (_flareCounter <= 0)
            {
                muzzleFlare.SetActive(false);
            }
        }
    }

    public void Shoot()
    {
       //Debug.Log("I shot");
        
       
       RaycastHit hit;
       if (Physics.Raycast(cam.position, cam.forward, out hit, range, validLayers ))
       {
           Debug.Log(hit.transform.name );

           if (hit.transform.tag == "Enemy")
           {
               Instantiate(damageEffect, hit.point, Quaternion.identity);
           }
           else
           {
                Instantiate(impactEffect, hit.point, Quaternion.identity);
           }
           
       }
       
       muzzleFlare.SetActive(true);
       _flareCounter = flareDisplayTime;

       _shotCounter = timeBetweenShots;

    }

    public void ShootHold()
    {
        _shotCounter -= Time.deltaTime;
        if (_shotCounter <= 0)
        {
            Shoot();
        }
    }
    
}
