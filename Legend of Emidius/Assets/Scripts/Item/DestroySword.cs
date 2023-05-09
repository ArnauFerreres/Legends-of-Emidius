using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroySword : MonoBehaviour
{
    
    public AudioClip armor;
    public GameObject armadura;

    private void Awake()
    {
        GetComponent<AudioSource>().clip = armor;
        
        //bossName = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            GetComponent<AudioSource>().Play();
            sphereCollider.enabled = false;
            Destroy(armadura);
        }
    }

    //public void DestroyArmor()
    //{
    //    Destroy(gameObject);
    //}


}
