using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPuzzleController : MonoBehaviour
{
    public void SelectPuzzle(int index)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetPuzzleIndex(index);
        }

        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");
}
