using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask rayMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool IsOverTrack()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f, rayMask))
        {
            return true;
        }
        else
        {
            //Didn't hit the track means we must've gone off the edge
        }

        return false;
    }
}
