using System.Collections;
using System.Collections.Generic;
// using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;

public enum HttpMethod
{
    POST,
    GET
}

public enum Router
{
    getRunners,
    postResults
}
    public class APIServices
    {
        
        private APIServices(){}
        private static APIServices _instance = null;
        public static APIServices Instance {
            get {
                if (_instance == null) {
                    _instance = new APIServices();
                }
                return _instance;
            }
        }

        private string getRouter(Router router)
        {
            switch (router)
            {
                case Router.getRunners: return "/runners";
                case Router.postResults: return "/demo-results";
                default: return "NULL";
            }
        }

        private string baseUrl = "http://localhost:1337/api";
        private string apiToken = "Bearer 9c92772e5fa9a3c577a581258785cfd90bd981d99afb0865c93f260c61af7ff9009b6a325eeede2ab46afd950c4683d6cf7b703d20d24a0228930f52a6379cb84f0dc02da5f05416f5889a02cefc6ee20c788b09c3d3b061d84467763e60ca0611330b7bf44e2c95f67ee9f4b92d37a14e6c3612c20392994d49a974c2c2a173";

        public IEnumerator Request<T>(Router router, HttpMethod method, System.Action<T> callback)
        {
            var req = new UnityWebRequest(baseUrl + getRouter(router), method.ToString());
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Authorization", apiToken);
            req.timeout = 30;

            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + req.error);
            }
            else
            {
                Debug.LogFormat("Received text: <color=yellow>{0}</color>", req.downloadHandler.text);
                Debug.Log("Received: error" + req.downloadHandler.error);
                Debug.Log("result: " + req.result);

                byte[] result = req.downloadHandler.data;
                string dataJSON = System.Text.Encoding.Default.GetString(result);
                T response = JsonUtility.FromJson<T>(dataJSON);
                callback(response);
            }

        }

        public IEnumerator PostResultRequest<T>(Router router, HttpMethod method, T parameter, System.Action callback)
        {
            var req = new UnityWebRequest(baseUrl + getRouter(router), method.ToString());
            string jsonData = JsonUtility.ToJson(parameter);
            Debug.Log("Request JSON: <color=white>"+jsonData+"</color>");

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Authorization", apiToken);
            req.SetRequestHeader("Content-Type", "application/json");
            req.timeout = 30;

            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + req.error);
            }
            else
            {
                Debug.LogFormat("Received text: <color=yellow>{0}</color>", req.downloadHandler.text);
                Debug.Log("Received: error" + req.downloadHandler.error);
                Debug.Log("result: " + req.result);
            }
        }
        
    }
