using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public int id;
    public float height = 165.0f;
    public float weight = 65.0f;
    public float age = 18.0f;
    gameManager GameManager;
    private Vector3 scaleVector;
    void Awake() 
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        GameManager = gameController.GetComponent<gameManager>();
        SetupObject();
    }
    // Start is called before the first frame update
    void Start()
    {
        scaleVector = new Vector3(weight/GameManager.advWeight, height/GameManager.advHeight, weight/GameManager.advWeight);
        transform.localScale = scaleVector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupObject()
    {
        this.id = DataModels.objectCount;
        this.weight = DataModels.weightList[DataModels.objectCount];
        this.height = DataModels.heightList[DataModels.objectCount];
        this.age = DataModels.ageList[DataModels.objectCount];
        Debug.LogFormat("set weight:<color=blue>{0}</color> height:<color=green>{1}</color> age:<color=red>{2}</color> for <color=yellow>object[{3}]</color>", DataModels.weightList[DataModels.objectCount].ToString(), DataModels.heightList[DataModels.objectCount].ToString(), DataModels.ageList[DataModels.objectCount].ToString(), DataModels.objectCount.ToString());
        DataModels.IncreaseObjectCount();
    }
    
}
