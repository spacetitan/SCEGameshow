using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPanel : UIPanel
{
    public TextMeshProUGUI scoreLabel = null;
    public GameObject template = null;
    public Transform templateParent = null;
    public Button exitButton = null;

    void Start()
    {
        this.SetCanvasGroupOff();
        UIManager.instance.AddPanel("GameOver", this);

        this.exitButton.onClick.AddListener(Application.Quit);
    }

    public override void OnOpenPanel()
    {
        base.OnOpenPanel();

        if(GameManager.instance.numPlayers > 1)
        {
            this.scoreLabel.text = "Final Scores!";
        }
        else
        {
            this.scoreLabel.text = "Final Score!";
        }

        for(int i = 0; i < GameManager.instance.numPlayers; i++)
        {
            InputBox box = Instantiate(this.template, this.templateParent).GetComponent<InputBox>();
            box.SetText(GameManager.instance.playerName[i], GameManager.instance.playerScore[i]);
            box.gameObject.SetActive(true);
        }

        this.exitButton.interactable = true;
    }
}
