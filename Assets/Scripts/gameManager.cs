
using System.Collections;
using System.Collections.Generic;
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
    public List<int> finishList;    
    public List<float> finishTime;
    private List<float> positionList = new List<float>(){
        0.0f,0.0f,0.0f,0.0f,0.0f
    };

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
        restartBtn.gameObject.SetActive(false);
        restartBtn.onClick.AddListener(restartGame);
    }

    void Update()
    {
        timming.text = Time.timeSinceLevelLoad.ToString("0.00");
    }

    void LateUpdate()
    {

        rankingString.text = findRanking();
        if (finishList!= null)
        {
            if (finishList.Count>=5)
            {
                restartBtn.gameObject.SetActive(true);
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
        if (finishList != null) 
        {
            if (finishList.Count > 0) {
                for (int i=0; i<finishList.Count; i++)
                {
                    result += DataModels.runnerList[finishList[i]].attributes.name+"-"+finishTime[i].ToString("0.00")+"\n";
                }
                return result;
            }
        }
        for (int i=0; i<positionList.Count; i++)
        {
            ObjectPosition temp = objectPositions.Find(item => item.position == positionList[i]);
            result += DataModels.runnerList[temp.id].attributes.name+"\n";
        }
        return result;
    }

    public void restartGame() 
    {
        finishList = null;
        finishTime = null;
        SceneManager.LoadScene(0);
        DataModels.objectCount = 0;
    }
}
