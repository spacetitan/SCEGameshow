using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartPanel : UIPanel
{
    public NumberCounter counter = null;
    public Button startButton = null;
    public CanvasGroup fadeCG = null;

    void Start()
    {
        UIManager.instance.AddPanel("Start", this);

        this.startButton.onClick.AddListener(OnClickStart);

        StartCoroutine(FadeBlackScreen());
    }

    void Update()
    {
        
    }

    void OnClickStart()
    {
        GameManager.instance.numPlayers = counter.num;
        this.ClosePanel();
        UIManager.instance.OpenPanel("Game");
    }

    public IEnumerator FadeBlackScreen()
    {
        while(true)
        {
            float num = Mathf.Lerp(this.fadeCG.alpha, 0, .25f);
            this.fadeCG.alpha = num;
            yield return null;

            if(this.fadeCG.alpha < .05f)
            {
                this.fadeCG.alpha = 0;
                this.isOpen = false;
                break;
            }
        }
    }
}
