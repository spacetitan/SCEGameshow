using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    void Awake()
    {
        instance = this;
    }

    public string url;

    public void GetAnswers(string ext)
    {
        StartCoroutine(GetData(ext));
    }

    private IEnumerator GetData(string ext)
    {
        UnityWebRequest req = new UnityWebRequest();
        req.downloadHandler = new DownloadHandlerBuffer();
        req.url = url + ext;
        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failure: " + req.error);
        }
        else
        {
            Debug.Log("Success");
        }
    }
}
