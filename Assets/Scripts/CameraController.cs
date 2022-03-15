using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private Vector3 point;
    // private new Transform camera;
    public float speedMod = 1.0f;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // offset = transform.position - target.transform.position;
        // camera = GetComponent<Transform>();

        // point = target.transform.position; // get target coords
        // transform.LookAt(point); // make the camera look to target
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(target.transform);
        // transform.Translate(Vector3.right * Time.deltaTime);
        // camera.position = Vector3.Lerp(camera.position, target.position, (posSpeed * Time.deltaTime));

        // camera.rotation = Quaternion.Lerp(camera.rotation, target.rotation, (rotSpeed * Time.deltaTime));
        // point = target.transform.position; // get target coords
        // transform.LookAt(point); // make the camera look to target
        // transform.RotateAround(point, new Vector3(0.0f,1.0f,0.0f), 60 * Time.deltaTime * speedMod);
    }

    void LateUpdate() 
    {
        // transform.position = target.transform.position + offset;
        // transform.RotateAround(point, new Vector3(0.0f,1.0f,0.0f), 20 * Time.deltaTime * speedMod);
        point = target.transform.position; // get target coords
        transform.LookAt(point); // make the camera look to target
        transform.RotateAround(point, Vector3.up, 16 * Time.deltaTime * speedMod);
    }
}
