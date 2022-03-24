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
    
    public static List<APIRequest.Singleresult> generateResults(){
        var results = new List<APIRequest.Singleresult>();
            for (int i=0; i< runnerListResults.Count; i++) {
                var result = new APIRequest.Singleresult{ 
                    rank = runnerListResults[i].rankResult,
                    runnername = runnerListResults[i].runner.attributes.name,
                    runTime =  runnerListResults[i].timeResult     
                }; 
                results.Add(result);
            }
        Debug.Log("results.cout :"+results.Count.ToString());
        return results;
    }
}
