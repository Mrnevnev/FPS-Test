using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public float range;

    public Transform cam;

    public LayerMask validLayers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
       //Debug.Log("I shot");
        
       
       RaycastHit hit;
       if (Physics.Raycast(cam.position, cam.forward, out hit, range, validLayers ))
       {
           Debug.Log(hit.transform.name );
       }
    }
}
