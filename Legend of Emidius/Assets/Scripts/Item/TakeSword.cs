using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSword : MonoBehaviour
{
    public GameObject swordLargePlayer;
    public GameObject swordSmall;
    // Start is called before the first frame update
    void Start()
    {
        swordLargePlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SwordLarge")
        {
            swordLargePlayer.SetActive(true);
            swordSmall.SetActive(false);
        }
    }
}
