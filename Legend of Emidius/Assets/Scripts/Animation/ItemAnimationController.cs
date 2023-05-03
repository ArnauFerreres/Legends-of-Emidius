using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimationController : MonoBehaviour
{
    public enum itemType { Generic, Door }

    [Header("Puzzle Settings")]
    public itemType currentItemType;
    [SerializeField] private bool inverseRotation;
    [SerializeField, Range(0f, 360.0f)] private float rotationLimit = 100f;
    int rotationValue;


    [Header("General Settings")]
    [SerializeField, Range(0f, 180.0f)] private float degreesPerSecond = 75f;
    [SerializeField, Range(0f, 5f)] private float verticalLenght = 0.75f;
    [SerializeField, Range(0f, 5f)] private float verticalSpeed = 0.5f;

    Vector3 startPosition;

    void Start()
    {
        if (inverseRotation)
        {
            rotationValue = -1;
            transform.Rotate(new Vector3(0, degreesPerSecond * rotationValue * Time.unscaledDeltaTime, 0), Space.World);
        }
        else
            rotationValue = 1;

        startPosition = transform.position;
    }

    void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (currentItemType == itemType.Generic)
        {
            transform.Rotate(new Vector3(0, degreesPerSecond * rotationValue * Time.unscaledDeltaTime, 0), Space.World);
        }
        else
        {
            if (inverseRotation)
            {
                if (Mathf.Round(Mathf.Abs(transform.localEulerAngles.y)) > 360 - rotationLimit)
                {
                    transform.Rotate(new Vector3(0, degreesPerSecond * rotationValue * Time.unscaledDeltaTime, 0), Space.World);
                }
            }
            else
            {
                if (Mathf.Round(Mathf.Abs(transform.localEulerAngles.y)) < rotationLimit)
                {
                    transform.Rotate(new Vector3(0, degreesPerSecond * rotationValue * Time.unscaledDeltaTime, 0), Space.World);
                }
            }
            return;
        }

        float VerticalY = Mathf.PingPong(verticalSpeed * Time.time, verticalLenght);
        transform.position = new Vector3(transform.position.x, startPosition.y + VerticalY, transform.position.z);
    }
}
