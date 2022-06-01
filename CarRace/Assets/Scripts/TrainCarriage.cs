using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarriage : MonoBehaviour
{
    public float howFarBack;
    public PathRecorder followObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot;
        transform.position = followObject.GetPosition(howFarBack, out rot);
        transform.rotation = rot;
    }
}
