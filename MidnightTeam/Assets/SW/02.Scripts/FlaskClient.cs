using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlaskClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetRequest("192.168.1.46:5000/test"));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            HttpInfo info = new HttpInfo();
            info.Set(RequestType.GET, "/test", (DownloadHandler downloadHandler) =>
            {
                print("OnReceiveGet : " + downloadHandler.text);
            });
            HttpManager.Get().SendRequest(info);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            HttpInfo info = new HttpInfo();
            info.Set(RequestType.FILE_UPLOAD, "/test", (DownloadHandler downloadHandler) =>
            {
                print("OnReceiveGet : " + downloadHandler.text);
                AAA aaa = JsonUtility.FromJson<AAA>(downloadHandler.text);
                print(aaa.message); 
            });
            HttpManager.Get().SendRequest(info);
        }
    }
    

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Received: " + www.downloadHandler.text);
            }
        }
    }
}