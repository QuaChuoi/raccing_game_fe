using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class APIResponses
{

    [Serializable]
    public class RandomResponse
    {
        // public string jsonrpc;
        public Result result;
        // public int id;
    }

    [Serializable]    
    public class Result
    {
        public Data random;
        public string bitsUsed;
        public string bitsLeft;
        public string requestsLeft;
        public string advisoryDelay;
    }

    [Serializable]
    public class Data
    {
        public int[] data;
        public string completionTime;
    }
}
