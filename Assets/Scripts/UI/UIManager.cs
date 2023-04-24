using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    void Awake()
    {
        instance = this;
    }

    private Dictionary<string, UIPanel> uIPanels = new Dictionary<string, UIPanel>();
    public void AddPanel(string name, UIPanel panel)
    {
        uIPanels.Add(name, panel);
    }

    public void RemovePanel(string name)
    {
        if(uIPanels.ContainsKey(name))
        {
            uIPanels.Remove(name);
        }
    }
    public void OpenPanel(string name)
    {
        if(uIPanels.ContainsKey(name))
        {
            uIPanels[name].OpenPanel();
        }
    }
    public void ClosePanel(string name)
    {
        if(uIPanels.ContainsKey(name))
        {
            uIPanels[name].ClosePanel();
        }
    }

    public void Start()
    {
        
    }

    
}
