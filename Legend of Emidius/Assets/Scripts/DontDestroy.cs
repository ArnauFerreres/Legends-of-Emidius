using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
