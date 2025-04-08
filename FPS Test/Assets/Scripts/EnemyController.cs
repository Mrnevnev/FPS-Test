using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerController _player;

    public float moveSpeed;

    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        float yStore = rb.linearVelocity.y;
        
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
        
        rb.linearVelocity = transform.forward * moveSpeed;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, yStore, rb.linearVelocity.z);
    }
}
