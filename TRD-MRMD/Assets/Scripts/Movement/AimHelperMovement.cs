using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHelperMovement : MonoBehaviour
{
    [Range(0.01f, 0.5f)]public float smoothness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");

        AimHelperManagement(horInput, verInput);
    }

    void AimHelperManagement(float horInput, float verInput)
    {
        AdjustHorizontal(horInput);
        AdjustVertical(verInput);

        
    }

    void AdjustHorizontal(float horInput)
    {
        if (Mathf.Abs(horInput) > 0.01f)
        {
            transform.localPosition += Vector3.right * horInput * smoothness * Time.deltaTime;

            if (transform.localPosition.x > 0.55f)
            {
                transform.localPosition = new Vector3(0.55f, 0, transform.localPosition.z);
            }

            if (transform.localPosition.x < -0.55f)
            {
                transform.localPosition = new Vector3(-0.55f, 0, transform.localPosition.z);
            }
        }
        else
        {
            if (transform.localPosition.x > 0.4f)
            {
                transform.localPosition += Vector3.left * smoothness * Time.deltaTime;
            }

            if (transform.localPosition.x < -0.4f)
            {
                transform.localPosition += Vector3.right * smoothness * Time.deltaTime;
            }
        }
        
    }

    void AdjustVertical(float verInput)
    {
        if (Mathf.Abs(verInput) > 0.01f)
        {
            transform.localPosition += Vector3.forward * verInput * smoothness * Time.deltaTime;

            if (transform.localPosition.z > 0.75f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 0, 0.75f);
            }
        }
        else
        {
            if (transform.localPosition.z > 0.1f)
            {
                transform.localPosition += Vector3.back * smoothness * Time.deltaTime;
            }
        }
    }
}
