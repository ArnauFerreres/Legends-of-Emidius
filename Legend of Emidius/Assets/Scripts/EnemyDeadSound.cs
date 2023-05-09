using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadSound : MonoBehaviour
{
    public CapsuleCollider capsulleCollider;
    public AudioClip deathEnemigos;
    private AudioSource audioSource;
    private bool isSoundPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        capsulleCollider = GetComponentInChildren<CapsuleCollider>();
        audioSource = gameObject.GetComponent<AudioSource>();
        // Asignamos el AudioClip al objeto AudioSource
        audioSource.clip = deathEnemigos;
    }

    // Update is called once per frame
    void Update()
    {
        if(capsulleCollider == null && !isSoundPlayed)
        {
            DeathSound();

        }
    }
    public void DeathSound()
    {
        audioSource.PlayOneShot(deathEnemigos);
        isSoundPlayed = true;

    }
}
