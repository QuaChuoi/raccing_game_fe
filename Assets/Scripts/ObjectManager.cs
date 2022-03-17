using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public int id;
    public float height = 165.0f;
    public float weight = 65.0f;
    public float age = 18.0f;
    public string name;
    public APIResponses.Datum runner;
    gameManager GameManager;
    private Vector3 scaleVector;
    Material material;
    public GameObject childObject;
    void Awake() 
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        GameManager = gameController.GetComponent<gameManager>();
        material = childObject.GetComponent<Renderer>().material;
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
        setObjectColor();
        this.id = DataModels.objectCount;
        this.runner = DataModels.runnerList[id];
        this.weight = runner.attributes.weight;
        this.height = runner.attributes.height;
        this.age = runner.attributes.age;
        this.name = runner.attributes.name;
        Debug.LogFormat("set weight:<color=blue>{0}</color> height:<color=green>{1}</color> age:<color=red>{2}</color> for <color=yellow>object[{3}]</color>", weight.ToString(), height.ToString(), age.ToString(), DataModels.objectCount.ToString()+"-"+name);
        DataModels.IncreaseObjectCount();
    }
    
    private void setObjectColor()
    {
        Color greenColor;
        ColorUtility.TryParseHtmlString(DataModels.runnerList[DataModels.objectCount].attributes.color, out greenColor);
        this.material.color = greenColor;
    }
}
