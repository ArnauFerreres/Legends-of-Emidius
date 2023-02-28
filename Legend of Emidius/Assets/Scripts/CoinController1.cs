using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController1 : MonoBehaviour, iTakeItem
{
    void iTakeItem.TakeItem()
    {
        GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateTotalCoins();
        //Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("CoinController1");
        }
    }
}
