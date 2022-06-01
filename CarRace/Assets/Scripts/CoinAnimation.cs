using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    float speedRotation= 4f;
    public Vector3 rotationAngle;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAngle * speedRotation * Time.deltaTime);    //rotate the coins
    }
    
}
