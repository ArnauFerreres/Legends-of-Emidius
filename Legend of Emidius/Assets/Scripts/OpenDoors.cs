using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoors : MonoBehaviour
{
    public AudioClip puertaEffect;
    public GameObject puerta1;
    public GameObject puerta2;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = puertaEffect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            Destroy(puerta1);
            Destroy(puerta2);
        }
    }
}
