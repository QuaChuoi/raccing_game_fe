
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public TextMeshProUGUI rankingString;
    public TextMeshProUGUI timming;

    public int trackLenght = 222;
    [Header("Speed Setting")]
    public float moveSpeed = 3.50f;
    public float topSpeed = 4.00f;
    public float botSpeed = 3.00f;
    public float topAcceleration = 0.10f;
    public float botAcceleration = 0.08f;
    [Tooltip("The limit speed will increase or decrease every time unit at timeTriggerSpeedChange")]
    private float accelerationChangeLimit = 0.1f;
    [Tooltip("How long the time to trigger speed change")]
    public float timeTriggerAccelerationChange = 5.0f;
    public float timeTriggerSpeedChange = 0.4f;
    public Vector3 movingVector;
    public float advHeight = 165.00f;
    public float advWeight = 65.00f;
    public float advAge = 18.00f;
    public float heightFactorAffect = 0.10f;
    public float weightFactorAffect = 0.20f;
    public float ageFactorAffect = 0.10f;
    public float startSpeedFactor = 0.1f;
    public Button restartBtn;
    private List<float> positionList = new List<float>(){
        0.0f,0.0f,0.0f,0.0f,0.0f
    };
    private bool isFinish;
    GameObject scoreBoard;
    ScoreTable scoreTable;

    public class ObjectPosition
    {
        public int id;
        public float position;

    }

    private List<ObjectPosition> objectPositions = new List<ObjectPosition>
    {
        new ObjectPosition {id = 0, position = 0.0f},
        new ObjectPosition {id = 1, position = 0.0f},
        new ObjectPosition {id = 2, position = 0.0f},
        new ObjectPosition {id = 3, position = 0.0f},
        new ObjectPosition {id = 4, position = 0.0f},
    };

    void Awake() 
    {
        isFinish = false;

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        scoreBoard = canvas.transform.GetChild(0).gameObject;
        scoreTable = scoreBoard.GetComponent<ScoreTable>();
        scoreBoard.gameObject.SetActive(false);
        restartBtn.gameObject.SetActive(false);
        restartBtn.onClick.AddListener(restartGame);

        // Debug.Log("runner list count: "+DataModels.runnerList.Count.ToString());
    }

    void Update()
    {
        timming.text = Time.timeSinceLevelLoad.ToString("0.00");
    }

    void LateUpdate()
    {
        rankingString.text = findRanking();
        if (DataModels.runnerListResults!=null && !isFinish)
        {
            if (DataModels.runnerListResults.Count>=5)
            {
                scoreTable.updateScoreboard();
                isFinish = true;
                StartCoroutine(finishRace());
            }
        }
    }
    
    public void getObjectPosition(int id, float position) 
    {
        positionList[id] = position;
        ObjectPosition stamp = objectPositions.Find(i => i.id == id);
        stamp.position = position;    
    }

    public string findRanking()
    {
        string result = "";
        positionList.Sort();
        positionList.Reverse();
        if (DataModels.runnerListResults != null) 
        {
            if (DataModels.runnerListResults.Count > 0) {
                for (int i=0; i<DataModels.runnerListResults.Count; i++)
                {
                    result += DataModels.runnerListResults[i].runner.attributes.name+"-"+DataModels.runnerListResults[i].timeResult.ToString("0.00")+"\n";
                }
                return result;
            }
            for (int i=0; i<DataModels.runnerList.Count; i++)
            {
                ObjectPosition temp = objectPositions.Find(item => item.position == positionList[i]);
                result += DataModels.runnerList[temp.id].attributes.name+"\n";
            }
        }
        return result;
    }

    public void restartGame() 
    {
        DataModels.objectCount = 0;
        DataModels.runnerList.Clear();
        DataModels.runnerListResults.Clear();

        SceneManager.LoadScene(0);
    }

    IEnumerator finishRace()
    {
        yield return new WaitForSeconds(5);
        scoreBoard.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }
}
