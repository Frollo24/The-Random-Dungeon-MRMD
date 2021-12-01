using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    public NavMeshAgent agent;
    public ThirdPersonMovement player;

    public float threshold = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent = gameObject.AddComponent<NavMeshAgent>();
        Debug.Log(agent.isOnNavMesh);

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
            FindObjectOfType<ThirdPersonMovement>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);
            transform.position -= new Vector3(1.5f, 0f, 0f);
        }
    }

    private void MoveEnemy()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < threshold)
            agent.SetDestination(player.transform.position);
    }
}
