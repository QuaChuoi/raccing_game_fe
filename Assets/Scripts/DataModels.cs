using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataModels
{   
    public static int[] heightList;
    public static int[] ageList;
    public static int[] weightList;
    public static int objectCount = 0;
    public static void IncreaseObjectCount()
    {
        objectCount++;
    }

    public static bool checkingData()
    {
        if ((weightList != null) && (heightList != null) && (ageList != null))
        {
            return (weightList.Length>0)&&(heightList.Length>0)&&(ageList.Length>0);
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
}
