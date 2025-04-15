using NUnit.Framework;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private PlayerController _player;

    public float moveSpeed;

    public Rigidbody rb;

    public float chaseRange = 15f, stopChasingRange = 5f;

    private float _strafeAmount;

    public Animator anim;

    public Transform[] patrolPoints;

    private int _currentPatrolPoint;

    public Transform pointsHolder;

    public float pointWaitTime = 3f;

    private float _waitCounter;

    private bool _isDead;

    public float currentHealth = 25f;

    public float waitToDisappear = 4f;

    public Transform shootPoint;
    
    public EnemyProjectile projectile;
    
    public float timeBetweenShots;

    private float _shotCounter;

    public float shotDamage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();

        _strafeAmount = Random.Range(-.75f, .75f);
        
       pointsHolder.SetParent(null);

       _waitCounter = Random.Range(.75f, 1.25f) * pointWaitTime;
       
       _shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead == true)
        {
            waitToDisappear -= Time.deltaTime;

            if (waitToDisappear <= 0)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);

                if (transform.localScale.x == 0)
                {
                    Destroy(gameObject);
                    Destroy(pointsHolder.gameObject);
                }
                
            }
            
            return;
        }

        float yStore = rb.linearVelocity.y;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance < chaseRange)
        {
            transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
            
            if (distance > stopChasingRange )
            {
                rb.linearVelocity = (transform.forward + (transform.right * _strafeAmount)) * moveSpeed;
                
                anim.SetBool("Moving", true);
                
            }else
            {
                rb.linearVelocity = Vector3.zero;
                
                anim.SetBool("Moving", false);
            }
            
            _shotCounter -= Time.deltaTime;
            if (_shotCounter < 0)
            {
                shootPoint.LookAt(_player.theCam.transform.position);
                
                EnemyProjectile newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation);

                newProjectile.damageAmount = shotDamage;
                
                _shotCounter = timeBetweenShots;
            }
            
        }else
        {
            if (patrolPoints.Length > 0)
            {
                // looks for the next patrol point
                if (Vector3.Distance(transform.position,new Vector3(patrolPoints[_currentPatrolPoint].position.x, transform.position.y, patrolPoints[_currentPatrolPoint].position.z)) < .25f)
                {
                    _waitCounter -= Time.deltaTime;

                    rb.linearVelocity = Vector3.zero;
                    anim.SetBool("Moving", false);
                    
                    //checks the wait timer to see if it can move to the next point
                    if (_waitCounter <= 0)
                    {
                        _currentPatrolPoint++; // goes to the next point
                        // Checks to see if the patrol array is at the end than sets to element 0
                        if (_currentPatrolPoint >= patrolPoints.Length)
                        {
                            _currentPatrolPoint = 0; // end of control points sets it back to element 0
                        }
                        
                        _waitCounter = Random.Range(.75f, 1.25f) * pointWaitTime;
                    }
                }
                else
                {
                    //cycles through patrol points if the enemy if out of range
                    transform.LookAt(new Vector3(patrolPoints[_currentPatrolPoint].position.x, transform.position.y,
                        patrolPoints[_currentPatrolPoint].position.z));

                    //Moves the rb towards the current patrol point
                    rb.linearVelocity = transform.forward * moveSpeed;

                    anim.SetBool("Moving", true);
                }
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            
                anim.SetBool("Moving", false);
            }
           
        }
        
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, yStore, rb.linearVelocity.z);
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");

            _isDead = true;
            rb.linearVelocity = Vector3.zero;
            GetComponent<Collider>().enabled = false;

            rb.isKinematic = true;
        }
    }
    
}
