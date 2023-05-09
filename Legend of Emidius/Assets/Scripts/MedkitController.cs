using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitController : MonoBehaviour, iTakeItem
{
    public AudioClip potionEffect;
    public GameObject potion;
    void iTakeItem.TakeItem()
    {
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        GetComponent<AudioSource>().Play();
        capsuleCollider.enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>().TakeHealth();
        Destroy(potion);
    }

}
