using System.Collections;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public RectTransform container = null;
    public CanvasGroup canvasGroup = null;
    protected bool isOpen = false;

    public void Start()
    {
        this.isOpen = false;
        this.canvasGroup.alpha = 0;
        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
    }

    public virtual void OpenPanel()
    {
        SetCanvesGroup(true);
    }

    public void ClosePanel()
    {
        SetCanvesGroup(false);
    }

    public void SetCanvesGroup(bool val)
    {
        StartCoroutine(FadeCG(val));
    }

    public void SetCanvasGroupOff()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnOpenPanel(){}
    public virtual void OnClosePanel(){}

    public IEnumerator FadeCG(bool val)
    {
        if(val)
        {
            while(true)
            {
                float num = Mathf.Lerp(this.canvasGroup.alpha, 1, .25f);
                this.canvasGroup.alpha = num;
                yield return null;

                if(this.canvasGroup.alpha > .95f)
                {
                    this.canvasGroup.alpha = 1;
                    this.isOpen = true;
                    OnOpenPanel();
                    break;
                }
            }
        }
        else
        {
            while(true)
            {
                float num = Mathf.Lerp(this.canvasGroup.alpha, 0, .25f);
                this.canvasGroup.alpha = num;
                yield return null;

                if(this.canvasGroup.alpha < .05f)
                {
                    this.canvasGroup.alpha = 0;
                    this.isOpen = false;
                    OnClosePanel();
                    break;
                }
            }
        }

        this.canvasGroup.interactable = val;
        this.canvasGroup.blocksRaycasts = val;
    }
}
