using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectables : MonoBehaviour
{

    public GameObject collText;
    public int Collected;
    public AudioSource collectSound;

     void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collectSound.Play();
            Collected += 1;
            Destroy(gameObject);
        }
    }
    
}
