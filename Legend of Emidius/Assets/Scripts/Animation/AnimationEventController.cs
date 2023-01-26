using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationEventController : MonoBehaviour
{
    public static Action onAnimationEvent;

    private void InvokeEvent()
    {
        onAnimationEvent?.Invoke();
    }

}
