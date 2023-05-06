using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 patrolRange;
    public int combatRange;
    public GameObject Player;
    public SphereCollider combatCollider;
    public float speed;

    bool attacking = false;
    Vector3 target;
    bool targetReached = false;
    Vector3 startingPos;

    public int Health = 3;
    int startHealth;
    bool retreating = false;

    public PlayerController playerRef;

    Vector3 lookPos;
    public GameObject hitbox;

    public Rigidbody RB;
    public GameObject SmallerEnemy;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = Health;
        playerRef = Player.GetComponent<PlayerController>();
        startingPos = transform.position;
        combatCollider.radius = combatRange;
        target = new Vector3(Random.Range(startingPos.x - (patrolRange.x / 2), startingPos.x + (patrolRange.x / 2)), startingPos.y, Random.Range(startingPos.z - (patrolRange.z / 2), startingPos.z + (patrolRange.z / 2)));
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if (startHealth > 1)
            {
                Instantiate(SmallerEnemy, transform.position + new Vector3(3, 1, 0), Quaternion.identity);
                Instantiate(SmallerEnemy, transform.position + new Vector3(-3, 1, 0), Quaternion.identity);
            }
            Destroy(gameObject);
        }
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);

        if (!attacking)
        {
            if (transform.position == target)
            {
                targetReached = true;
            }
            if (!targetReached)
            {
                lookPos = target - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target, speed / 1.5f * Time.deltaTime);
            }
            else
            {
                targetReached = false;
                target = new Vector3(Random.Range(startingPos.x - (patrolRange.x / 2), startingPos.x + (patrolRange.x / 2)), startingPos.y, Random.Range(startingPos.z - (patrolRange.z / 2), startingPos.z + (patrolRange.z / 2)));
            }
        }
        else if (retreating)
        {
            lookPos = Player.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, -lookPos * 1000, speed * 1.5f * Time.deltaTime);
        }
        else
        {
            lookPos = Player.transform.position - transform.position;

            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }

    }

    IEnumerator retreat()
    {
        retreating = true;

        yield return new WaitForSeconds(1.5f);

        retreating = false;
    }
    public void TakeDamage()
    {
        Health -= 1;
        RB.AddRelativeForce(0, 200, -800);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attacking = true;
        }
        if (other.gameObject.tag == "hitbox")
        {
            TakeDamage();
            hitbox.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attacking = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRef.TakeDamage();
            StartCoroutine(retreat());
        }
    }
}
