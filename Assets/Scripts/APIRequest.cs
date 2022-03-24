using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class APIRequest
{
    [Serializable]
    public class Singleresult
    {
        public int rank;
        public string runnername;
        public float runTime;
    }

    [Serializable]
    public class Data
    {
        public List<Singleresult> singleresult;
    }

    [Serializable]
    public class Root
    {
        public Data data;
    }
}
