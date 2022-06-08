using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                                                    // reference to the car tronsform

    public Vector3 distance;                                                           // distance between camera and cars
    public float smoothValue;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;          // Find the car with tag
        //distance = target.position - transform.position;                        // calculate distance
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.y >= 0)                                               // check if the target position greater than 0 
        {
            Follow();
        }
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Follow()
    {
        Vector3 currentPos = transform.position;                                // camera current position

        Vector3 targetPos = target.position - distance;                               //calculate the target position

        transform.position = Vector3.Lerp(currentPos, targetPos, smoothValue * Time.deltaTime);            // move the camera
    }
}
