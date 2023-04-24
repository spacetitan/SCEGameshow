using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberCounter : MonoBehaviour
{
    public TextMeshProUGUI numText = null;
    public Button leftButton = null;
    public Button rightButton = null;

    public int num{get; private set;} = 0;
    public int min = 0;
    public int max = 9;

    void Start()
    {
        this.leftButton.onClick.AddListener(this.OnClickLeft);
        this.rightButton.onClick.AddListener(this.OnClickRight);

        this.num = 1;
        UpdateText();
    }

    void OnClickLeft()
    {
        if(num > min)
        {
            num--;
        }
        UpdateText();
    }

    void OnClickRight()
    {
        if(num < max)
        {
            num++;
        }
        UpdateText();
    }

    void UpdateText()
    {
        this.numText.text = num.ToString();
    }
}
