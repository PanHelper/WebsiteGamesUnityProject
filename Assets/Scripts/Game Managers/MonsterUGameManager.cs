using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MonsterUGameManager : MonoBehaviour
{
    #region Variables
    
    // GameObject Variables
    [Header("Screens")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject questionScreen, resultScreen;

    // TextMeshProUGUI Variables
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI qText;
    [SerializeField] private TextMeshProUGUI[] answersText;

    // Boolean Variables
    public static bool gameActive, gameStarted;

    // String Variables
    [Space(20)]
    [SerializeField] private List<string> questions;

    // Integer Variables
    [SerializeField] private List<int> answers;
    private int nextQuest;

    // Script Variables
    private InputActions input;

    #endregion Variables

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

        nextQuest = 0;

        NextQuestion();
        OpenStart();
    }

    // Restarts the game from the beginning
    public void Restart()
    { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    #region Menu Operations
    
    // Deactivates every menu
    void CloseMenus()
    {
        startScreen.SetActive(false);
        questionScreen.SetActive(false);
        resultScreen.SetActive(false);
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
    public void OpenResultScreen()
    {
        gameActive = false;
        CloseMenus();
        resultScreen.SetActive(true);
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
    public void Answer(int ans)
    {
        answers[ans] += 1;
        NextQuestion();
    }

    // Chooses the next question and loads the question screen
    public void NextQuestion()
    {
        // Either moves to the next question or ends the game
        if(nextQuest < questions.Count)
        {
            qText.text = questions[nextQuest];
            // Update answer options
            OpenQuestionScreen();
            nextQuest++;
        }
        else { OpenResultScreen(); }
    }

    #endregion
}
