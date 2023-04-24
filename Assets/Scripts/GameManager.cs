using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }

    public List<string> questions = new List<string>();
    public Dictionary<string, int> answers{get; private set;} = new Dictionary<string, int>();

    public int numPlayers = 0;
    public int currentRound = 0;

    public List<string> playerName = new List<string>();
    public List<float> playerScore = new List<float>(); 
    void Start()
    {
        SetDummyTable();
    }

    void Update()
    {
        
    }

    public void SetTable(Dictionary<string, int> table)
    {
        answers = new Dictionary<string, int>();
        foreach(KeyValuePair<string, int> kvp in table)
        {
            answers.Add(kvp.Key, kvp.Value);
        }
    }

    public string GetQuestion()
    {
        if(questions.Count > 0)
        {
            string val = questions[0];
            questions.RemoveAt(0);
            return val;
        }
        else
        {
            Debug.LogError("Error: No more questions");
            return "";
        }
    }

    public string GetQuestion(int num)
    {
        if(questions.Count > 0)
        {
            string val = questions[num];
            questions.RemoveAt(num);
            return val;
        }
        else
        {
            Debug.LogError("Error: No more questions");
            return "";
        }
    }

    public void SetDummyTable()
    {
        answers = new Dictionary<string, int>();
        answers.Add("dummy", 100);
        for(int i = 1; i <= 24; i++)
        {
            answers.Add("dummy" + i.ToString(), i);
        }
    }

    public int CheckAnswer(string answer)
    {
        int points = 0;
        string key = answer.ToLower();
        if(this.answers.ContainsKey(key))
        {
            points = answers[key];
        }
        return points;
    }
}
