using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject[] puzzlePieces;
    private Sprite[] puzzleImages;

    private PuzzlePiece[,] Matrix = new PuzzlePiece[GameVariables.MaxRows, GameVariables.MaxColumns];

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

    private void Start()
    {
        puzzleIndex = -1;
    }

    private void Update()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay" && puzzleIndex > 0)
        {
            LoadPuzzle();
            GameStarted();
        }
    }
    
    private void GameStarted()
    {
        int index = Random.Range(0, GameVariables.MaxSize);

        puzzlePieces[index].SetActive(false);

        for (int row = 0; row < GameVariables.MaxRows; row++)
        {
            for (int column = 0; column < GameVariables.MaxColumns; column++)
            {
                if (puzzlePieces[row * GameVariables.MaxColumns].activeInHierarchy)
                {
                    Vector3 point = GetScreenCoordinateFromViewPort(row, column);

                    puzzlePieces[row * GameVariables.MaxColumns + column].transform.position = point;

                    Matrix[row, column] = new PuzzlePiece();

                    Matrix[row, column].GameObject = puzzlePieces[row * GameVariables.MaxColumns + column];

                    Matrix[row, column].OriginalRow = row;
                    Matrix[row, column].OriginalColumn = column;
                } else {
                    Matrix[row, column] = null;
                }
            }
        }

        Shuffle();
        
        gameState = GameState.Playing;
    }

    private void Shuffle()
    {
        for (int row = 0; row < GameVariables.MaxRows; row++)
        {
            for (int column = 0; column < GameVariables.MaxColumns; column++)
            {
                if (Matrix[row, column] == null) continue;

                int randomRow = Random.Range(0, GameVariables.MaxRows);
                int randomColumn = Random.Range(0, GameVariables.MaxColumns);

                Swap(row, column, randomRow, randomColumn);
            }
        }
    }
    
    private void Swap(int row, int column, int randomRow, int randomColumn)
    {
        PuzzlePiece temp = Matrix[row, column];
        
        (
            Matrix[row, column],
            Matrix[randomRow, randomColumn]
        ) = (
            Matrix[randomRow, randomColumn],
            Matrix[row, column]
        );

        Matrix[randomRow, randomColumn] = temp;

        if (Matrix[row, column] != null)
        {
            Matrix[row, column].GameObject.transform.position = GetScreenCoordinateFromViewPort(row, column);

            Matrix[row, column].CurrentRow = row;
            Matrix[row, column].CurrentColumn = column;
        }

        Matrix[randomRow, randomColumn].GameObject.transform.position = GetScreenCoordinateFromViewPort(randomRow, randomColumn);

        Matrix[randomRow, randomColumn].CurrentRow = randomRow;
        Matrix[randomRow, randomColumn].CurrentColumn = randomColumn;
        
    }

    private Vector3 GetScreenCoordinateFromViewPort(int row, int column)
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.2309f * row, 1 - 0.233f * column, 0));

        point.z = 0;

        return point;
    }

    private void LoadPuzzle()
    {
        puzzleImages = Resources.LoadAll<Sprite>("Sprites/BG " + puzzleIndex);

        puzzlePieces = GameObject.Find("PuzzleHolder").GetComponent<Holder>().puzzlePieces;

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            puzzlePieces[i].GetComponent<SpriteRenderer>().sprite = puzzleImages[i];
        }
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
        puzzleIndex = index;
    }
}
