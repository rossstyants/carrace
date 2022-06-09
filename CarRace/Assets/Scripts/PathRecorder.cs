using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRecorder : MonoBehaviour
{
    //distance moved each frame
    List<float> distanceMoved = new List<float>();
     List<Vector3> previousPositions = new List<Vector3>();
     List<Quaternion> previousRots = new List<Quaternion>();

    [SerializeField] List<TrainCarriage> followers = new List<TrainCarriage>();


    public float TotalDistanceTravelled;
    public float TotalDistanceRemoved;
    [SerializeField] private float recordHowFarBack;
    public bool IsRecording = false;
    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;

        Vector3 pos = transform.position;
        //foreach(TrainCarriage tc in followers)
        //{
        //    pos.z -= tc.howFarBack;
        //    distanceMoved.Add(tc.howFarBack);
        //    previousPositions.Add(pos);
        //    previousRots.Add(tc.transform.rotation);
        //}
    }

    // Update is called once per frame
    Vector3 prevPos;
    void Update()
    {
        if (!IsRecording)
            return;

        Vector3 moveDistance = transform.position - prevPos;
        prevPos = transform.position;
        float distanceMovedThisTurn = moveDistance.magnitude;
        distanceMoved.Add(distanceMovedThisTurn);
        previousPositions.Add(transform.position);
        previousRots.Add(transform.rotation);
        TotalDistanceTravelled += distanceMovedThisTurn;

        if ((TotalDistanceTravelled- TotalDistanceRemoved) > recordHowFarBack)
        {
            TotalDistanceRemoved += distanceMoved[0];
            distanceMoved.RemoveAt(0);
            previousPositions.RemoveAt(0);
            previousRots.RemoveAt(0);
        }
    }

    public Vector3 GetPosition(float howFarBack, out Quaternion rot, out bool hasValue)
    {
        hasValue = true;
        int i = distanceMoved.Count - 1;
        if (i < 0)
        {
            hasValue = false;
            rot = Quaternion.identity;
            return Vector3.zero;
        }
        float totalDistance = 0f;
        i++;
        do
        {
            i--;
            totalDistance += distanceMoved[i];
        } while (i > 0 && totalDistance < howFarBack);

        if (i > 0)
        {
            //try to get position as a ratio between two closest?
            float difference = totalDistance - howFarBack;
            //what ratio was that of that last move
            float ratio = (distanceMoved[i] - difference) / distanceMoved[i];
            //

            rot = previousRots[i];
            if (i < (previousPositions.Count - 1))
                return Vector3.Lerp(previousPositions[i + 1], previousPositions[i], ratio);
            else
                return previousPositions[i];
        }

        hasValue = false;
        rot = Quaternion.identity;
        return Vector3.zero;
    }
}
