using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public static Vector3 startPosition = new Vector3(2, 0.84f, 0);

    public float speed = 1.5f;
    public float smoothingTime = 0.1f;
    private float smoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movementDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (movementDir.magnitude >= 0.1f)
        {
            //Make it look where the camera is looking
            float targetAngle = Mathf.Atan2(movementDir.x, movementDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothingTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 newMoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            newMoveDir.Normalize();

            controller.Move(newMoveDir * speed * Time.deltaTime);
        }    
    }
}
