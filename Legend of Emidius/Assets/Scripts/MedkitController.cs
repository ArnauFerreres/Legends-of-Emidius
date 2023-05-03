using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitController : MonoBehaviour, iTakeItem
{
    void iTakeItem.TakeItem()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>().TakeHealth();
        Destroy(gameObject);
    }
}
