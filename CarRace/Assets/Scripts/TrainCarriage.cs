using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarriage : MonoBehaviour
{
    public float howFarBack;
    public PathRecorder followObject;
    public Vector3 initialOffset;

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = followObject.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot;
        bool hasValue;
        Vector3 pos = followObject.GetPosition(howFarBack, out rot, out hasValue);
        if (hasValue)
        {
            transform.position = pos;
            transform.rotation = rot;
        }
        else
        {
            //just maintain the offset to the front carriage until we have values
            transform.position = followObject.transform.position - initialOffset;
        }
    }
}
