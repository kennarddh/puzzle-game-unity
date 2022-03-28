using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject[] puzzlePieces;
    private Sprite[] puzzleImages;

    private Vector3 screenPositionToAnimate;
    private PuzzlePiece pieceToAnimate;
    private int toAnimateRow, toAnimateColumn;

    private float animationSpeed = 10f;

    private int puzzleIndex;

    private GameState gameState;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Update()
    {
        
    }
    
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void SetPuzzleIndex(int index)
    {
        this.puzzleIndex = index;
    }
}
