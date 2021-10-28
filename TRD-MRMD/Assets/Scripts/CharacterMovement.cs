using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotationSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

            transform.position += movementDir * speed * Time.deltaTime;

            Quaternion rotateTo = Quaternion.LookRotation(movementDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotationSpeed * Time.deltaTime);
        }

        
        
    }
}
