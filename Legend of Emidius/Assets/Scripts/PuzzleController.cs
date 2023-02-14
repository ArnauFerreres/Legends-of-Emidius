using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public enum puzzleType { Generic, Door}
    [SerializeField] private puzzleType currentPuzzleType;

    public void finishPuzzle()
    {
        switch (currentPuzzleType)
        {
            case puzzleType.Generic:
                break;
            case puzzleType.Door:
                gameObject.GetComponent<ItemAnimationController>().enabled = true; ;
                break;
        }
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
