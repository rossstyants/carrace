using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour {

    public Transform copyObject;

    public bool x = true;
    public bool y = true;
    public bool z = true;

    public Vector3 offset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 rot = transform.localEulerAngles;
        if (x)
            rot.x = copyObject.transform.localEulerAngles.x;
        if (y)
            rot.y = copyObject.transform.localEulerAngles.y;
        if (z)
            rot.z = copyObject.transform.localEulerAngles.z;

        rot += offset;

        transform.localEulerAngles = rot;


    }
}
