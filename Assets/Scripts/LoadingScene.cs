using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public bool checkingData = false;
    APIServices apiService = APIServices.Instance;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(apiService.RequestRandomArr(5,50,80,true,DataModels.updateWeightList));
        StartCoroutine(apiService.RequestRandomArr(5,150,185,true,DataModels.updateHeightList));
        StartCoroutine(apiService.RequestRandomArr(5,16,30,true,DataModels.updateAgeList));
        StartCoroutine(WaitUntilIncident());
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkingData)
        {
            checkingData = DataModels.checkingData();
        }
    }

    IEnumerator WaitUntilIncident()
    {
        // yield return new WaitForSecondsRealtime(5);
        yield return new WaitUntil(() => checkingData);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
