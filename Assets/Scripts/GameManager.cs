using System;
using SimpleJSON;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionData
{
    public Dictionary<string, int> answers = new Dictionary<string, int>();
    public string question;

    public void AddQuestion(string key, int val)
    { 
        answers.Add(key, val);
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }

    //public List<string> questions = new List<string>();
    //public Dictionary<string, int> answers{get; private set;} = new Dictionary<string, int>();

    public List<QuestionData> baseQuestionData = new List<QuestionData>();
    public List<QuestionData> questionData = new List<QuestionData>();

    public int numPlayers = 0;
    public int currentRound = 0;

    public List<string> playerName = new List<string>();
    public List<float> playerScore = new List<float>(); 

    void Start()
    {
        SetTable();
    }

    public void SetTable()
    {
        string jsonBlob = File.ReadAllText("Assets/Scripts/questions.json");
        JSONObject nodes = (JSONObject)JSON.Parse(jsonBlob);
        Debug.Log(nodes.ToString());
        foreach(JSONObject x in nodes["questions"])
        {
            QuestionData qd = new QuestionData();
            qd.question = x["question"];
            Dictionary<string, int> a = new Dictionary<string, int>();
            foreach (JSONObject y in x["answers"])
            {
                foreach (KeyValuePair<string, JSONNode> temp in y)
                {
                    qd.AddQuestion(temp.Key, (int)temp.Value);
                    //Debug.Log("Added: " + temp.Key + ": " + temp.Value);
                }
            }

            baseQuestionData.Add(qd);
            questionData.Add(qd);
        }

       // print(questionData);
    }

   public QuestionData GetQuestionData()
    {
        if (questionData.Count > 0)
        {
            QuestionData val = questionData[0];
            questionData.RemoveAt(0);
            return val;
        }
        else
        {
            Debug.LogError("Error: No more questions");
            return null;
        }
   }

    /*public string GetQuestion(int num)
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
    }*/

    /*public void SetDummyTable()
    {
        answers = new Dictionary<string, int>();
        answers.Add("dummy", 100);
        for(int i = 1; i <= 24; i++)
        {
            answers.Add("dummy" + i.ToString(), i);
        }

        print(answers);
    }*/

    public void SetAnswers()
    {
        //answers = new Dictionary<string, int>();

    }

    
}
