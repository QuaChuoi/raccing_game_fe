using System.Collections;
using System.Collections.Generic;
// using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;


    public class APIServices
    {
        public enum HttpMethod
        {
            POST,
            GET
        }
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
        // private string baseUrl = "https://api.random.org/json-rpc/4/invoke";
        private string baseUrl = "http://localhost:1337/api/runners";
        private string apiToken = "Bearer 5b478b2fc02e9a8e24525172e4d47fad93e0dd5be5e8af8404c40be16abe3ba2f22f88679edf586780db1bd57533be74850181c810352f48d55e9923ec9b5ade40491ddf33023a873c4d8cf2b51c08c72f8e812e9b571bc289569199c1041e0e5540bc2ff1eb0b011292060ba808a4e9efc5a578579747992177179294a6070f";
        // public string apiKey = "4f938c03-0a47-49a3-8822-91ee6c02f546";
        APIRequest.Root apiRequest = new APIRequest.Root();

        public IEnumerator Request<T>(HttpMethod method, System.Action<T> callback)
        {
            var req = new UnityWebRequest(baseUrl, method.ToString());
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
                // Debug.Log(dataJSON);
                T response = JsonUtility.FromJson<T>(dataJSON);
                callback(response);
            }

        }
 
        // public IEnumerator Request(HttpMethod method)
        // {
        //     var req = new UnityWebRequest(baseUrl, method.ToString());
        //     req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //     req.SetRequestHeader("Authorization", apiToken);
        //     req.timeout = 30;

        //     yield return req.SendWebRequest();
        //     if (req.result == UnityWebRequest.Result.ConnectionError)
        //     {
        //         Debug.Log("Error While Sending: " + req.error);
        //     }
        //     else
        //     {
        //         Debug.LogFormat("Received text: <color=yellow>{0}</color>", req.downloadHandler.text);
        //         Debug.Log("Received: error" + req.downloadHandler.error);
        //         Debug.Log("result: " + req.result);

        //         byte[] result = req.downloadHandler.data;
        //         string dataJSON = System.Text.Encoding.Default.GetString(result);
        //         Debug.Log(dataJSON);
        //     }

        // }
        

        // public IEnumerator RequestRandomArr(int number, int min, int max, bool replacement, Action<int[]> callback)
        // {   
        //     APIRequest.Params parameters = new APIRequest.Params();
        //     // parameters.apiKey = apiKey;
        //     parameters.n = number;
        //     parameters.min = min;
        //     parameters.max = max;
        //     parameters.replacement = replacement;
        //     apiRequest.@params = parameters;
            
        //     string jsonData = JsonUtility.ToJson(apiRequest);
        //     Debug.Log("Request JSON: <color=green>"+jsonData+"</color>");
        //     Debug.Log("Enum ------ " + HttpMethod.GET.ToString());

        //     var req = new UnityWebRequest(baseUrl, HttpMethod.POST.ToString());
        //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        //     req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        //     req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //     req.SetRequestHeader("Content-Type", "application/json");
        //     req.timeout = 30;

        //     //Send the request then wait here until it returns
        //     yield return req.SendWebRequest();

        //     if (req.result == UnityWebRequest.Result.ConnectionError)
        //     {
        //         Debug.Log("Error While Sending: " + req.error);
        //     }
        //     else
        //     {
        //         Debug.LogFormat("Received text: <color=yellow>{0}</color>", req.downloadHandler.text);
        //         // Debug.LogFormat("Reviced data: <color=green>{0}</color>", req.downloadHandler.data);
        //         Debug.Log("Received: error" + req.downloadHandler.error);
        //         Debug.Log("result: " + req.result);

        //         byte[] result = req.downloadHandler.data;
        //         string dataJSON = System.Text.Encoding.Default.GetString(result);
        //         APIResponses.RandomResponse randomResponse = JsonUtility.FromJson<APIResponses.RandomResponse>(dataJSON);
        //         callback(randomResponse.result.random.data);
        //     }
        // }
    }
