using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;
    public Vector3 direction { get; set; }
    public float speed;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag != "Player")
            Destroy(gameObject);
    }

}