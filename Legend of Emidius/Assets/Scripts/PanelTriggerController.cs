using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTriggerController : MonoBehaviour
{
    public GameObject panel;
    public float triggerDistance = 5.0f;

    private CanvasGroup canvasGroup;
    private bool isPlayerNear = false;

    void Start()
    {
        canvasGroup = panel.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (distance <= triggerDistance && !isPlayerNear)
        {
            canvasGroup.alpha = 1.0f;
            isPlayerNear = true;
            panel.SetActive(true);
        }
        else if (distance > triggerDistance && isPlayerNear)
        {
            canvasGroup.alpha = 0.0f;
            isPlayerNear = false;
            panel.SetActive(false);
        }
    }
}
