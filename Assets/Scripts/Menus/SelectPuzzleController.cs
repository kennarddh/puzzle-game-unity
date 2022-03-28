using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPuzzleController : MonoBehaviour
{
    public void SelectPuzzle(string name)
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");
}
