using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputBox : MonoBehaviour
{
    public GamePanel gamePanel = null;
    public TMP_InputField inputField = null;
    public TextMeshProUGUI pointsText = null;
    public Button nextButton = null;
    public CanvasGroup imageCG = null;

    public float points = 0;

    void Start()
    {
        
    }

    public void CheckAnswer()
    {
        points = gamePanel.CheckAnswer(inputField.text);
        pointsText.text = points.ToString();
    }

    public void SetText(string name, float points)
    {
        this.inputField.text = name;
        this.points = points;
        this.pointsText.text = points.ToString();
    }

    public void Reset()
    {
        this.inputField.text = "";
        this.pointsText.text = "";
    }

    public void SetArrow(bool val)
    {
        if(val)
        {
            this.imageCG.alpha = 1;
        }
        else
        {
            this.imageCG.alpha = 0;
        }
    }
}
