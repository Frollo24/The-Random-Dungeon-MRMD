using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject loot;
    public GameObject fountain;

    public void InstantiateLoot(Transform place)
    {
        Instantiate(loot, place.position, place.rotation);
    }

    public void InstantiateFountain(Transform place)
    {
        Instantiate(fountain, place.position, place.rotation);
    }
}
