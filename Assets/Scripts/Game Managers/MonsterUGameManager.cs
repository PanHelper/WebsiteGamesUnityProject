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

    // Image Variables
    [SerializeField] private Image resultSprite;

    // Sprite Variables
    [SerializeField] private Sprite eekSprite;
    [SerializeField] private Sprite hssSprite;
    [SerializeField] private Sprite pnkSprite;

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

    #endregion Menu Operations

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

    #endregion Input

    #region Questions

    // Sets up the text for the answer choices to each question
    private void InitializeAnswers()
    {
        answers = new string[7, 3];

        /* 
            answers[#, 0] --> eek
            answers[#, 1] --> hss
            answers[#, 2] --> pnk
        */
        
        answers[0, 0] = "Curling up with a good book or brainstorming ideas.";
        answers[0, 1] = "Strategizing my next big plan or pushing myself to improve.";
        answers[0, 2] = "Practicing my moves or perfecting my look.";

        answers[1, 0] = "Staying behind the scenes to brainstorm creative solutions.";
        answers[1, 1] = "Organizing everyone to ensure we win at all costs.";
        answers[1, 2] = "Leading the charge and making sure we all look amazing doing it.";
        
        answers[2, 0] = "Be subtle and catch them off guard with a clever setup.";
        answers[2, 1] = "Go for maximum intimidation right away.";
        answers[2, 2] = "Charm them into letting their guard down—then pounce!";
        
        answers[3, 0] = "Focus on outsmarting the competition.";
        answers[3, 1] = "Crush the competition—no room for second place!";
        answers[3, 2] = "Play fair, but never forget to dazzle.";
        
        answers[4, 0] = "Functional, comfy, and maybe a little quirky.";
        answers[4, 1] = "Sleek, bold, and totally intimidating.";
        answers[4, 2] = "Trendy, flashy, and always coordinated with my crew.";
        
        answers[5, 0] = "Being thoughtful and welcoming to everyone.";
        answers[5, 1] = "Focusing on strength, loyalty, and victory.";
        answers[5, 2] = "Looking fabulous and sticking together.";
        
        answers[6, 0] = "\"Brains over brawn, every time.\"";
        answers[6, 1] = "\"Dominate or disappear.\"";
        answers[6, 2] = "\"Look good, scare better.\"";
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
            resultSprite.sprite = eekSprite;
        }
        else if(max == hss)
        {
            resultText.text = "You've been sorted into HSS!";
            resultSprite.sprite = hssSprite;
        }
        else
        {
            resultText.text = "You've been sorted into PNK!";
            resultSprite.sprite = pnkSprite;
        }
        
        OpenResultScreen();
    }

    #endregion Questions
}
