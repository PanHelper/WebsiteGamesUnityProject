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
    [Header("Answer Buttons")] [SerializeField] private GameObject[] answerButtons;

    // Vector3 Variables
    [SerializeField] private List<Vector3> answerLocations;

    // TextMeshProUGUI Variables
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI qText;
    [SerializeField] private TextMeshProUGUI[] answersText;
    [Header("Results")] [SerializeField] private TextMeshProUGUI resultText;

    // SpriteRenderer Variables
    [SerializeField] private SpriteRenderer resultSprite;

    // Boolean Variables
    public static bool gameActive, gameStarted;

    // String Variables
    [Header("Questions")] [SerializeField] private List<string> questions;
    [SerializeField] private string[,] answers;

    // Integer Variables
    private int nextQuest;

    [Header("Distributions")]
    [SerializeField] private int eek;
    [SerializeField] private int hss;
    [SerializeField] private int pnk;

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

        eek = 0;
        hss = 0;
        pnk = 0;

        InitializeAnswers();
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

    // Sets up the text for the answer choices to each question
    private void InitializeAnswers()
    {
        answers = new string[5, 3];
        
        answers[0, 0] = "00";
        answers[0, 1] = "01";
        answers[0, 2] = "02";

        answers[1, 0] = "10";
        answers[1, 1] = "11";
        answers[1, 2] = "12";
        
        answers[2, 0] = "20";
        answers[2, 1] = "21";
        answers[2, 2] = "22";
        
        answers[3, 0] = "30";
        answers[3, 1] = "31";
        answers[3, 2] = "32";
        
        answers[4, 0] = "40";
        answers[4, 1] = "41";
        answers[4, 2] = "42";
    }

    // Determines which category to increment in the distribution
    public void Answer(int ans)
    {
        // the answer best aligns with EEK
        if(ans == 0) { eek++; }

        // the answer best aligns with HSS
        else if(ans == 1) { hss++; }

        // the answer best aligns with PNK
        else { pnk++; }

        NextQuestion();
    }

    // Chooses the next question and loads the question screen
    public void NextQuestion()
    {
        // Either moves to the next question or ends the game
        if(nextQuest < questions.Count)
        {
            // Randomizes answer locations
            List<Vector3> temp = new List<Vector3>(answerLocations);
            foreach (GameObject button in answerButtons)
            {
                int i = Random.Range(0, temp.Count);
                button.transform.localPosition = temp[i];
                temp.RemoveAt(i);
            }
            
            qText.text = questions[nextQuest];

            // Update answer options
            for(int i = 0; i < answersText.Length; i++)
            {
                answersText[i].text = answers[nextQuest, i];
            }

            OpenQuestionScreen();
            nextQuest++;
        }
        else { UpdateResults(); }
    }

    // Updates the Results Screen
    private void UpdateResults()
    {
        // determines the highest score
        int max = Mathf.Max(eek, Mathf.Max(hss, pnk));
        if(max == eek)
        {
            resultText.text = "You've been sorted into EEK!";
        }
        else if(max == hss)
        {
            resultText.text = "You've been sorted into HSS!";
        }
        else
        {
            resultText.text = "You've been sorted into PNK!";
        }
        
        OpenResultScreen();
    }

    #endregion
}
