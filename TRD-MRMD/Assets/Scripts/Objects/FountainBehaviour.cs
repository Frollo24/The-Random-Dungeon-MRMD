using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainBehaviour : MonoBehaviour
{
    private ThirdPersonMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().RestoreHealth(1);
        }
    }
}
