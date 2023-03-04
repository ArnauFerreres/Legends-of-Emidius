using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestryWall : MonoBehaviour
{
    public BoxCollider bossWall;

    private void Start()
    {
        bossWall= GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject.GetComponent<BoxCollider>());

        }
    }
}
