using System.Collections;
using System.Collections.Generic;
// using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;

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
        private string baseUrl = "https://api.random.org/json-rpc/4/invoke";
        public string apiKey = "4f938c03-0a47-49a3-8822-91ee6c02f546";
        APIRequest.Root apiRequest = new APIRequest.Root();

        public IEnumerator RequestRandomArr(int number, int min, int max, bool replacement, Action<int[]> callback)
        {   
            APIRequest.Params parameters = new APIRequest.Params();
            parameters.apiKey = apiKey;
            parameters.n = number;
            parameters.min = min;
            parameters.max = max;
            parameters.replacement = replacement;
            apiRequest.@params = parameters;
            
            string jsonData = JsonUtility.ToJson(apiRequest);
            Debug.Log("Request JSON: <color=green>"+jsonData+"</color>");

            var req = new UnityWebRequest(baseUrl, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            req.timeout = 30;

            //Send the request then wait here until it returns
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + req.error);
            }
            else
            {
                Debug.LogFormat("Received text: <color=yellow>{0}</color>", req.downloadHandler.text);
                // Debug.LogFormat("Reviced data: <color=green>{0}</color>", req.downloadHandler.data);
                Debug.Log("Received: error" + req.downloadHandler.error);
                Debug.Log("result: " + req.result);

                byte[] result = req.downloadHandler.data;
                string dataJSON = System.Text.Encoding.Default.GetString(result);
                APIResponses.RandomResponse randomResponse = JsonUtility.FromJson<APIResponses.RandomResponse>(dataJSON);
                callback(randomResponse.result.random.data);
            }
        }

        public IEnumerator Request()
        {   
            string jsonData = "{ \"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"id\":11,\"params\":{ \"apiKey\":\""+apiKey+"\",\"n\":5,\"min\":130,\"max\":230,\"replacement\":true } }";
            Debug.Log("jsonData: "+jsonData);

            var req = new UnityWebRequest(baseUrl, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + req.error);
            }
            else
            {
                Debug.Log("Received text: " + req.downloadHandler.text);
                Debug.LogFormat("Reviced data: <color=green>{0}</color>", req.downloadHandler.data);
                Debug.Log("Received: error" + req.downloadHandler.error);

                byte[] result = req.downloadHandler.data;
                string dataJSON = System.Text.Encoding.Default.GetString(result);
                APIResponses.RandomResponse randomResponse = JsonUtility.FromJson<APIResponses.RandomResponse>(dataJSON);
                // PrintList(randomResponse.result.random.data);
            }
        }

        public void PrintList(int[] input)
        {
            Debug.Log("callllll");
            foreach (int chil in input) {
                Debug.Log("====" + chil.ToString());
            }
        }
    }
