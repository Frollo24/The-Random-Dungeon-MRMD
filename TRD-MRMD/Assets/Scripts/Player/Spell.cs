using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;
    public Vector3 direction { get; set; }
    public float speed;

    [SerializeField] private Transform particles;
    [SerializeField] private new ParticleSystem particleSystem;
    public GameObject explosion;

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particles = particleSystem.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Explode()
    {
        // Instantiate an explosion
        var exploding = Instantiate(explosion, transform.position, transform.rotation);

        // Move the particle system
        particles.parent = exploding.transform;
        var emission = particleSystem.emission;
        emission.rateOverTime = 0;

        // Destroy the objects
        Destroy(gameObject);
        Destroy(exploding, 2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            other.GetComponent<EnemyHealth>().TakeDamage(damage);

        if (!other.CompareTag("Player") && !other.isTrigger)
            Explode();
    }

}
