using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerController _player;

    public float moveSpeed;

    public Rigidbody rb;

    public float chaseRange = 15f, stopChasingRange = 5f;

    private float strafeAmount;

    public Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();

        strafeAmount = Random.Range(-.75f, .75f);
    }

    // Update is called once per frame
    void Update()
    {

        float yStore = rb.linearVelocity.y;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance < chaseRange)
        {
            transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
            
            if (distance > stopChasingRange )
            {
                rb.linearVelocity = (transform.forward + (transform.right * strafeAmount)) * moveSpeed;
                
                anim.SetBool("Moving", true);
                
            }else
            {
                rb.linearVelocity = Vector3.zero;
                
                anim.SetBool("Moving", false);
            }
        }else
        {
            rb.linearVelocity = Vector3.zero;
            
            anim.SetBool("Moving", false);
        }
        
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, yStore, rb.linearVelocity.z);
    }
}
