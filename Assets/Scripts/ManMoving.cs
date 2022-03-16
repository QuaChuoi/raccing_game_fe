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
    private bool isFinish = false;
    /// Awake is called when the script instance is being loaded.
    void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        GameManager = gameController.GetComponent<gameManager>();
        animation = gameObject.GetComponent<Animator>();
        objectManager = GetComponent<ObjectManager>();
        point = GameManager.transform.position;
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

        if (transform.position.z <= 312)
        {
            updateAcceleration();
            updateMovementSpeed();

            transform.Translate(GameManager.movingVector * currentSpeed * Time.deltaTime, Space.Self);

            // speedTag.text = currentSpeed.ToString("0.00");  
            // speedTag.text = objectManager.id.ToString();      
            // speedTag.text = acceleration.ToString("0.00");   
            speedTag.text = objectManager.name;

            if ((!isFinish) && (transform.position.z >= 305)) {
                GameManager.finishList.Add(objectManager.id);
                isFinish = true;
                // Debug.Log("add "+ objectManager.id.ToString());
            }
        } else {
            // if (!isFinish) {
            //     GameManager.addFinishList(objectManager.id);
            // }
            animation.Play("Base Layer.Stand", 0, 0.25f);
            // animation.Play("Base Layer.Swing Dancing", 0, 0.25f);
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
}
