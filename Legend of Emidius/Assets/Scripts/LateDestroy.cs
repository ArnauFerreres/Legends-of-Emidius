using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateDestroy : MonoBehaviour
{
    void Update()
    {
        Invoke("DestroyObject", 1.3f);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
