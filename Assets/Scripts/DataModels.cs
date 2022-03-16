using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataModels
{   
    public static int[] heightList;
    public static int[] ageList;
    public static int[] weightList;
    public static List<APIResponses.Datum> runnerList = new List<APIResponses.Datum>();
    public static int objectCount = 0;
    public static void IncreaseObjectCount()
    {
        objectCount++;
    }

    public static bool checkingData()
    {
        if(runnerList!=null)
        {
            return (runnerList.Count>0);
        } else return false;
    }

    public static void updateWeightList(int[] input)
    {
        weightList = input;
    }
    public static void updateHeightList(int[] input)
    {
        heightList = input;
    }
    public static void updateAgeList(int[] input)
    {
        ageList = input;
    }

    public static void getRunners(APIResponses.Root runners)
    {
        runnerList = runners.data;
        // foreach (APIResponses.Datum runner in runnerList)
        // {
        //     Debug.Log("runner: "+runner.id+"-"+runner.attributes.name);
        // }
    }
}
