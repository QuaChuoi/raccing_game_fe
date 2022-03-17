using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class APIResponses
{

    [Serializable]
    public class Attributes
    {
        public string runner_id;
        public string name;
        public float height;
        public float weight;
        public int age;
        public string color;
    }

    [Serializable]
    public class Datum
    {
        public int id;
        public Attributes attributes;
    }

    [Serializable]
    public class Root
    {
        public List<Datum> data;
    }
}
