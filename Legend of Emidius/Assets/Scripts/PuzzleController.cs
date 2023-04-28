using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
public class PuzzleController : MonoBehaviour
{
    public enum puzzleType { Generic, Door}
    [SerializeField] private puzzleType currentPuzzleType;

    [Header("Door Puzzle settings")]
    [SerializeField] private CinemachineFreeLook playerCamera;
    [SerializeField] private CinemachineVirtualCamera doorPuzzleCamera;

    public void finishPuzzle()
    {
        switch (currentPuzzleType)
        {
            case puzzleType.Generic:
                break;
            case puzzleType.Door:
                gameObject.GetComponent<ItemAnimationController>().enabled = true;
                StartCoroutine(ResetCameraPriority(playerCamera.m_XAxis.m_MaxSpeed, playerCamera.m_YAxis.m_MaxSpeed));
                playerCamera.Priority = 0;
                doorPuzzleCamera.Priority = 1;
                playerCamera.m_XAxis.m_MaxSpeed = 0;
                playerCamera.m_YAxis.m_MaxSpeed = 0;
                Time.timeScale = 0;
                break;
        }
    }

    IEnumerator ResetCameraPriority(float xAxis, float yAxis)
    {
        yield return new WaitForSecondsRealtime(3);
        playerCamera.Priority = 1;
        doorPuzzleCamera.Priority = 0;

        yield return new WaitForSecondsRealtime(1);
        playerCamera.m_XAxis.m_MaxSpeed = xAxis;
        playerCamera.m_YAxis.m_MaxSpeed = yAxis;
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        UIController.onUpdateCoins += finishPuzzle;
    }
    private void OnDisable()
    {
        UIController.onUpdateCoins -= finishPuzzle;
    }
}
