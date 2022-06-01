using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRecorder : MonoBehaviour
{
    //distance moved each frame
     List<float> distanceMoved = new List<float>();
    List<Vector3> previousPositions = new List<Vector3>();
    List<Quaternion> previousRots = new List<Quaternion>();

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    Vector3 prevPos;
    void Update()
    {
        Vector3 moveDistance = transform.position - prevPos;
        prevPos = transform.position;
        distanceMoved.Add(moveDistance.magnitude);
        previousPositions.Add(transform.position);
        previousRots.Add(transform.rotation);
    }

    public Vector3 GetPosition(float howFarBack, out Quaternion rot)
    {
        int i = distanceMoved.Count - 1;
        float totalDistance = 0f;
        do
        {
            totalDistance += distanceMoved[i];
            i--;
        } while (i > 0 && totalDistance < howFarBack);

        if (i > 0)
        {
            rot = previousRots[i];
            return previousPositions[i];
        }

        rot = Quaternion.identity;
        return Vector3.zero;
    }
}
