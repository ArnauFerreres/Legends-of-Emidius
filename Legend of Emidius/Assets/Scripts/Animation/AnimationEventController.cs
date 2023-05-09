using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimationEventController : MonoBehaviour
{
    public static Action onAnimationEvent;
    public AudioClip sword;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sword;
    }
    private void InvokeEvent()
    {
        onAnimationEvent?.Invoke();
        //PlaySound();
    }
    void PlaySound()
    {
        audioSource.PlayOneShot(sword);
    }

}
