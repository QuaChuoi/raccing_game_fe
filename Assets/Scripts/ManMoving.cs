using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMoving : MonoBehaviour
{
    gameManager GameManager;
    Animator animation;
    ObjectManager objectManager;
    private Vector3 point;
    private float currentSpeed, topSpeed, botSpeed, topAcc, botAcc, startSpeed;
    private float time = 0.0f;
    private float miniTime = 0.0f;
    private float acceleration = 0.0f;
    public TextMesh speedTag;
    private bool isFinish;
    private bool isStopRunning;
    /// Awake is called when the script instance is being loaded.
    void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        GameManager = gameController.GetComponent<gameManager>();
        animation = gameObject.GetComponent<Animator>();
        objectManager = GetComponent<ObjectManager>();
        point = GameManager.transform.position;

        isStopRunning = false;
        isFinish = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        conculateSpeedParams();
        currentSpeed = startSpeed;
        animation.speed = 0.445f * (currentSpeed / 2.0f);
        // animation.Play("Base Layer.Swing Dancing", 0, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        miniTime += Time.deltaTime;

        if (transform.position.z <= ((GameManager.trackLenght + 16) - setSpaceBetween()))
        {
            updateAcceleration();
            updateMovementSpeed();

            transform.Translate(GameManager.movingVector * currentSpeed * Time.deltaTime, Space.Self); 
            speedTag.text = objectManager.name;

            if ((!isFinish) && (transform.position.z >= GameManager.trackLenght)) {
                this.isFinish = true;
                int rank = DataModels.runnerListResults==null ? 1 : DataModels.runnerListResults.Count + 1;
                DataModels.addRunnerListResults(rank, objectManager.runner, Time.timeSinceLevelLoad);
            }
        } else {
            if (!isStopRunning)
            {
                isStopRunning = true;
            if (objectManager.runner.id == DataModels.runnerListResults[0].runner.id)
            {
                animation.Play("Swing Dancing");
            } else
            {
                animation.Play("Stand");
            }
            }
        }
        GameManager.getObjectPosition(objectManager.id, transform.position.z);
    }

    private void updateMovementSpeed()
    {
        if (miniTime >= GameManager.timeTriggerSpeedChange)
        {

            miniTime -= GameManager.timeTriggerSpeedChange;
            updateCurrentSpeed(currentSpeed += acceleration);
        }
    }
    private void updateAcceleration()
    {
        // Debug.Log("time: " + time + "time set: " + GameManager.timeTriggerAccelerationChange);
        if (time >= GameManager.timeTriggerAccelerationChange)
        {
            time -= GameManager.timeTriggerAccelerationChange;
            acceleration = Random.Range(-botAcc, topAcc);
            // Debug.Log("acce change at -- " + Time.realtimeSinceStartup + "time :" + time);
        }
    }

    private void updateCurrentSpeed(float newSpeed)
    {
        if (newSpeed <= botSpeed)
        {
            currentSpeed = botSpeed;
        }
        else
        {
            if (currentSpeed >= topSpeed)
            {
                currentSpeed = topSpeed;
            }
            else
            {
                currentSpeed = newSpeed;
            }
        }
        animation.speed = 0.445f * (currentSpeed / 2.00f);
        // speedTag.text = topSpeed.ToString("0.00");
    }
    private void conculateSpeedParams()
    {
        startSpeed = 
            GameManager.moveSpeed * (
                GameManager.startSpeedFactor * (
                    GameManager.heightFactorAffect * (objectManager.height / GameManager.advHeight - 1.00f)
                    + GameManager.weightFactorAffect * (-(objectManager.weight / GameManager.advWeight - 1.00f))
                    + GameManager.ageFactorAffect * (objectManager.age / GameManager.advAge - 1.00f)
                )
                // + (1.00f - GameManager.startSpeedFactor * (GameManager.heightFactorAffect + GameManager.weightFactorAffect + GameManager.ageFactorAffect))
                +1.00f
            );
        topSpeed =
            GameManager.topSpeed * (
                GameManager.heightFactorAffect * (objectManager.height / GameManager.advHeight - 1.00f)
                + GameManager.weightFactorAffect * (-(objectManager.weight / GameManager.advWeight - 1.00f))
                + GameManager.ageFactorAffect * (-(objectManager.age / GameManager.advAge - 1.00f))
                // + (1.00f - (GameManager.heightFactorAffect + GameManager.weightFactorAffect + GameManager.ageFactorAffect))
                +1.00f
            );
        botSpeed =
            GameManager.botSpeed * (
                GameManager.weightFactorAffect * (-(objectManager.weight / GameManager.advWeight -1.00f))
                + GameManager.ageFactorAffect * (objectManager.age / GameManager.advAge - 1.00f)
                // + (1.00f - (GameManager.heightFactorAffect + GameManager.weightFactorAffect + GameManager.ageFactorAffect))
                +1.00f
            );
        topAcc =
            GameManager.topAcceleration * (
                GameManager.heightFactorAffect * (objectManager.height / GameManager.advHeight - 1.00f)
                + GameManager.weightFactorAffect * (-(objectManager.weight / GameManager.advWeight - 1.00f))
                + GameManager.ageFactorAffect * (objectManager.age / GameManager.advAge - 1.00f)
                // + (1.00f - (GameManager.heightFactorAffect + GameManager.weightFactorAffect + GameManager.ageFactorAffect))
                +1.00f
            );
        botAcc =
            GameManager.botAcceleration * (
                GameManager.heightFactorAffect * (-(objectManager.height / GameManager.advHeight - 1.00f))
                + GameManager.weightFactorAffect * (-(objectManager.weight / GameManager.advWeight - 1.00f))
                + GameManager.ageFactorAffect * (objectManager.age / GameManager.advAge - 1.00f)
                // + (1.00f - (GameManager.heightFactorAffect + GameManager.weightFactorAffect + GameManager.ageFactorAffect))
                +1.00f
            );
    }

    private int setSpaceBetween()
    {
        var n = DataModels.runnerListResults!=null ? DataModels.runnerListResults.Count : 0;
        return 2*n;
    }
}
