using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PassTroughFogWall : MonoBehaviour
    {
        public AudioClip bossMusic;
        BoxCollider boss;


        private void Start()
        {
            GetComponent<AudioSource>().clip = bossMusic;
            boss = GetComponent<BoxCollider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponent<AudioSource>().Play();

            }
        }
    }
}