using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TriviaGameManager : MonoBehaviour
{
    #region Variables
    
    // GameObject Variables
    [SerializeField] private GameObject startScreen, questionScreen, scoreScreen;

    // TextMeshProUGUI Variables

    // Boolean Variables
    public static bool gameActive, gameStarted;
    [SerializeField] private List<bool> answers;

    // String Variables
    [SerializeField] private List<string> questions;

    // Integer Variables
    private int currQuest, questsDone, score;

    // Script Variables
    private InputActions input;

    #endregion

    // Called when the game is loaded
    private void Awake()
    {
        input = new InputActions();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        gameStarted = false;

        questsDone = 0;
        score = 0;

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

    #region Input
    
    // Called when the script is enabled
    private void OnEnable()
    {
        input.Enable();
        input.UI.CloseStart.performed += OnStartPerformed;
    }

    // Called when the script is disabled
    private void OnDisable()
    {
        input.Disable();
        input.UI.CloseStart.performed -= OnStartPerformed;
    }

    // Called when any of the binds associated with CloseStart in input are used
    private void OnStartPerformed(InputAction.CallbackContext context)
    {
        // only opens the level selection menu if the start menu is active
        if(startScreen.activeSelf) { OpenQuestionScreen(); }
    }

    #endregion

    #region Questions

    // Determines whether the player got the question correct, and updates the score
    public void Answer(bool ans)
    {
        // Checks for correct answer
        if(ans == answers[currQuest])
        {
            score++;
            // show the player they were right
        }
        else
        {
            // show the player they were wrong
        }

        questions.RemoveAt(currQuest);
        answers.RemoveAt(currQuest);
        questsDone++;

        // Either moves to the next question or ends the game
        if(questsDone < 5) {}
        else 
        {
            // update score screen

            OpenScoreScreen();
        }
    }

    #endregion
}
