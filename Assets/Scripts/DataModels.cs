using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerResult
{
    public int rankResult;
    public APIResponses.Datum runner;
    public float timeResult;
}

public static class DataModels
{   
    public static List<RunnerResult> runnerListResults;
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

    public static void getRunners(APIResponses.Root runners)
    {
        runnerList = runners.data;
    }

    public static void addRunnerListResults(int rank, APIResponses.Datum runner, float time)
    {
        RunnerResult runnerResult = new RunnerResult{ rankResult = rank, runner = runner, timeResult = time};
        if(runnerResult!= null){
            DataModels.runnerListResults.Add(runnerResult);
        } else {
            Debug.Log("NUL RUNNER RESULT");
        }
    }
}
