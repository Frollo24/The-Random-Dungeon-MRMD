using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    public NavMeshAgent agent;
    public ThirdPersonMovement player;

    public int enemyDamage = 10;

    public float detectionThreshold = 6.0f;
    [SerializeField] private Vector3 destination;
    [SerializeField] private bool isDestinationSet;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent = gameObject.AddComponent<NavMeshAgent>();
        Debug.Log(agent.isOnNavMesh);
        //agent.speed = speed;

        player = FindObjectOfType<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0.5f, 0f, 0f) * speed * Time.deltaTime;

        /*
        if (Input.GetMouseButtonDown(0))
        {
            MoveEnemy();
        }
        */

        if (player != null)
            MoveEnemy();
        else
            player = FindObjectOfType<ThirdPersonMovement>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
            transform.position -= new Vector3(1.5f, 0f, 0f);
        }
    }

    private void MoveEnemy()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectionThreshold)
        {
            destination = player.transform.position;
            destination.y = transform.position.y;
            agent.SetDestination(destination);
            isDestinationSet = false;
        }
        else
        {
            if (!isDestinationSet)
            {
                destination += Random.insideUnitSphere * detectionThreshold;
                destination.y = transform.position.y;

                NavMesh.SamplePosition(destination, out NavMeshHit hit, detectionThreshold, 1);
                destination = hit.position;

                agent.SetDestination(hit.position);
                isDestinationSet = true;
            }
            else
            {
                if (Vector3.Distance(transform.position, destination) < 1.5f)
                {
                    isDestinationSet = false;
                }
            }
        }
    }
}
