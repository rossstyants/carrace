using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFace : MonoBehaviour {

    public GameObject faceToObject;

	// Use this for initialization
	void Start () {
//		faceToObject = ARManager.Instance.ARCamera.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetPos = faceToObject.transform.position;
        targetPos.y = transform.position.y;

        transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
    }
}
