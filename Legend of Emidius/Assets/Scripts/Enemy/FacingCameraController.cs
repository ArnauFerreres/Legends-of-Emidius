using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCameraController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward);
    }
}
