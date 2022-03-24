using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public bool checkingData = false;
    APIServices apiService = APIServices.Instance;
    // Start is called before the first frame update
    private void Awake() {
        DataModels.runnerListResults = new List<RunnerResult>();
        DataModels.runnerList = new List<APIResponses.Datum>();
    }

    void Start()
    {
        // call web requests here
        StartCoroutine(apiService.Request<APIResponses.Root>(Router.getRunners, HttpMethod.GET, DataModels.getRunners));
        // StartCoroutine(apiService.Request<APIResponses.Root>(Router.postResults, HttpMethod.GET, (response) => {}));

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
        yield return new WaitUntil(() => checkingData);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
