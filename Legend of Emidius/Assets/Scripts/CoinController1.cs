using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController1 : MonoBehaviour, iTakeItem
{
    public AudioClip sonidoPuerta;
    SphereCollider lever;
    private void Start()
    {
        GetComponent<AudioSource>().clip = sonidoPuerta;
        lever = GetComponent<SphereCollider>();
    }
    void iTakeItem.TakeItem()
    {
        GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateTotalCoins();
        //Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            lever.enabled = false;
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("CoinController1");

        }

    }
}
