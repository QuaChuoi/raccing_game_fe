using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class APIRequest
{
    [Serializable]
    public class Params
    {
        public string apiKey;
        public int n;
        public int min;
        public int max;
        public bool replacement;
    }

    [Serializable]
    public class Root
    {
        public string jsonrpc = "2.0";
        public string method = "generateIntegers";
        public int id = 11;
        public Params @params;
    }
}
