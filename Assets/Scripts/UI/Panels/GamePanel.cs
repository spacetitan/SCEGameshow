using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GamePanel : UIPanel
{
    public TextMeshProUGUI questionLabel = null;
    public TextMeshProUGUI questionText = null;
    public List<InputBox> answerFields = new List<InputBox>();
    public Button nextRoundButton = null;
    public TextMeshProUGUI timerText = null;
    public TextMeshProUGUI buttonText = null;

    public List<string> questions = new List<string>();

    private int round = 0;
    private int question = 0;
    public float time = 15.0f;
    private float timer = 0.0f;

    private bool gameStarted = false;

    void Start()
    {
        this.SetCanvasGroupOff();
        UIManager.instance.AddPanel("Game", this);

        InitializeUI();
    }

    void InitializeUI()
    {
        for(int i = 0; i < this.answerFields.Count; i++)
        {
            if(i != this.answerFields.Count-1)
            {
                answerFields[i].nextButton.onClick.AddListener(NextQuestion);
            }
            else
            {
                answerFields[i].nextButton.onClick.AddListener(EndRound);
            }
        }

        this.nextRoundButton.onClick.AddListener(NextRound);
        SetNextButtonText();
        this.nextRoundButton.interactable = false;
    }

    void Update()
    {
        if(this.gameStarted)
        {
            timer += Time.smoothDeltaTime;
            if(timer > time)
            {
                timer = time;
            }

            this.timerText.text = (this.time - this.timer).ToString("F0");

            if((this.time - this.timer) == 0 && this.question == 5)
            {
                EndRound();
            }
            else if((this.time - this.timer) == 0)
            {
                NextQuestion();
            }
        }
    }

    public override void OnOpenPanel()
    {
        base.OnOpenPanel();

        StartRound();
        EventSystem.current.SetSelectedGameObject(this.answerFields[this.question-1].inputField.gameObject);
    }

    void GrabQuestions(bool random)
    {
        questions.Clear();

        if(!random)
        {
            if(GameManager.instance.questions.Count >= 5)
            {
                for(int i = 0; i < 5; i++)
                {
                    questions.Add(GameManager.instance.GetQuestion());
                }
            }
            else
            {
                for(int i = 0; i < GameManager.instance.questions.Count; i++)
                {
                    questions.Add(GameManager.instance.GetQuestion());
                }
            }
        }
        else
        {
            if(GameManager.instance.questions.Count >= 5)
            {
                for(int i = 0; i < 5; i++)
                {
                    int val = Random.Range(0, GameManager.instance.questions.Count);
                    questions.Add(GameManager.instance.GetQuestion(val));
                }
            }
            else
            {
                for(int i = 0; i < GameManager.instance.questions.Count; i++)
                {
                    questions.Add(GameManager.instance.GetQuestion());
                }
            }
        }

        
    }

    void StartRound()
    {
        this.gameStarted = true;
        GrabQuestions(true);

        this.round++;
        this.question = 1;

        foreach(InputBox box in this.answerFields)
        {
            box.Reset();
        }

        SetQuestion();
        SetFields();
        SetNextButtonText();
        this.timer = 0;
    }

    void NextQuestion()
    {
        this.question++;

        SetQuestion();
        SetFields();
        this.timer = 0;
    }

    void EndRound()
    {
        this.gameStarted = false;
        this.timerText.text = "";

        foreach(InputBox box in this.answerFields)
        {
            box.inputField.interactable = false;
            box.nextButton.interactable = false;
            box.SetArrow(false);
        }

        StartCoroutine(ShowAnswers());
    }

    void NextRound()
    {
        GameManager.instance.playerScore.Add(GetPoints());
        if(GameManager.instance.numPlayers > this.round)
        {
            StartRound();
        }
        else
        {
            this.ClosePanel();
            UIManager.instance.OpenPanel("GameOver");
        }
    }

    float GetPoints()
    {
        float val = 0;

        foreach(InputBox box in this.answerFields)
        {
            val += box.points;
        }

        return val;
    }

    private IEnumerator ShowAnswers()
    {
        foreach(InputBox box in this.answerFields)
        {
            box.CheckAnswer();
            yield return new WaitForSeconds(.3f);
        }

        this.nextRoundButton.interactable = true;
    }

    void SetQuestion()
    {
        this.questionLabel.text = "Question " + this.question.ToString() + ": ";
        this.questionText.text = this.questions[this.question-1];
    }

    void SetNextButtonText()
    {
        if(GameManager.instance.numPlayers > round)
        {
            this.buttonText.text = "Next Round";
        }
        else
        {
            this.buttonText.text = "End Game";
        }
    }

    void SetFields()
    {
        foreach(InputBox box in this.answerFields)
        {
            box.inputField.interactable = false;
            box.nextButton.interactable = false;
            box.SetArrow(false);
        }

        this.answerFields[this.question-1].inputField.interactable = true;
        this.answerFields[this.question-1].nextButton.interactable = true;
        this.answerFields[this.question-1].SetArrow(true);

        EventSystem.current.SetSelectedGameObject(this.answerFields[this.question-1].inputField.gameObject);
    }
}
