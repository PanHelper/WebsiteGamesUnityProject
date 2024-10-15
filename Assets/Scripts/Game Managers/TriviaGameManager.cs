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
    [Header("Screens")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject questionScreen, scoreScreen;

    // TextMeshProUGUI Variables
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI qText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI feedbackText;

    // Boolean Variables
    public static bool gameActive, gameStarted;
    [Space(20)]
    [SerializeField] private List<bool> answers;

    // String Variables
    [SerializeField] private List<string> questions;
    [SerializeField] private List<string> feedback;

    // Integer Variables
    private int currQuest, questsDone, score;

    // Script Variables
    private InputActions input;

    // Float Variables
    [Header("Animation Length")]
    [SerializeField] private float correctSecs;
    [SerializeField] private float wrongSecs;

    // Animator Variables
    [Header("(In)Correct Animator")]
    [SerializeField] private Animator anim;

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

        questsDone = 0;
        score = 0;

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
        scoreText.text = (score + "/5");
        feedbackText.text = feedback[score];
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
            StartCoroutine(Drawing(1, correctSecs));
        }
        else { StartCoroutine(Drawing(-1, wrongSecs)); }

        questions.RemoveAt(currQuest);
        answers.RemoveAt(currQuest);
        questsDone++;
    }

    // Tells the player whether they got the question correct
    private IEnumerator Drawing(int state, float waitTime)
    {
        anim.SetInteger("State", state);

        yield return new WaitForSeconds(waitTime);
        
        anim.SetInteger("State", 0);

        NextQuestion();
    }

    // Chooses the next question and loads the question screen
    public void NextQuestion()
    {
        // Either moves to the next question or ends the game
        if(questsDone < 5)
        {
            currQuest = Random.Range(0, questions.Count);
            qText.text = questions[currQuest];
            OpenQuestionScreen();
        }
        else 
        {
            // update score screen

            OpenScoreScreen();
        }
    }

    #endregion
}
