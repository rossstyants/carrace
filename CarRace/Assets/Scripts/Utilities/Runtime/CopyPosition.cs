using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public GameObject objectToCopy;
    public Vector3 offset;

    public bool setRotation;
    public Vector3 worldEuler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setRotation)
            transform.eulerAngles = worldEuler;
        transform.position = objectToCopy.transform.position + offset;
    }
}
