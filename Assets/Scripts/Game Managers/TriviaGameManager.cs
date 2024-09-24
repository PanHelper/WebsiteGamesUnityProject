using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/* using UnityEngine.InputSystem; */

public class TriviaGameManager : MonoBehaviour
{
    #region Variables
    
    // GameObject Variables
    [SerializeField] private GameObject startScreen, questionScreen, scoreScreen;

    // TextMeshProUGUI Variables

    // Boolean Variables
    public static bool gameActive, gameStarted;

    // Integer Variables

    // BoxCollider2D Variables

    // SpriteRenderer Variables

    // Sprite Variables

    // Image Variables

    // Script Variables

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        gameStarted = false;
        OpenStart();
    }

    #region Menu Operations
    
    // Deactivates every menu
    void CloseMenus()
    {
        startScreen.SetActive(false);
        questionScreen.SetActive(false);
        scoreScreen.SetActive(false);
    }

    // Opens the Start Screen
    public void OpenStart()
    {
        gameActive = false;
        gameStarted = false;
        CloseMenus();
        startScreen.SetActive(true);
    }

    // Opens the Question Screen
    public void OpenQuestionScreen()
    {
        gameActive = false;
        CloseMenus();
        questionScreen.SetActive(true);
        gameActive = true;
        gameStarted = true;
    }

    // Opens the Score Screen
    public void OpenScoreScreen()
    {
        gameActive = false;
        CloseMenus();
        scoreScreen.SetActive(true);
    }

    #endregion
}
