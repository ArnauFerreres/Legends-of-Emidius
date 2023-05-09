using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadSound : MonoBehaviour
{

    public AudioClip deathEnemigos;
    public Rigidbody rigidbodyEnemies;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyEnemies = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        // Asignamos el AudioClip al objeto AudioSource
        audioSource.clip = deathEnemigos;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbodyEnemies != null)
        {
            DeathSound();
        }
    }
    public void DeathSound()
    {
        audioSource.PlayOneShot(deathEnemigos);
    }
}
