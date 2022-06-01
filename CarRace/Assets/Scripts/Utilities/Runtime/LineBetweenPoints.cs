using UnityEngine;
using System;
using System.Collections;

[ExecuteInEditMode]
public class LineBetweenPoints : MonoBehaviour
{
    //creates a 3d line (cyclinder) between two points...

    public bool logOn;

    public GameObject StartPoint, EndPoint;

    public GameObject cylinder;

    public float thickness = 1.0f;

    //---------------------------------------------------------
    void Start ()
    {
    }

    //---------------------------------------------------------
    void Update2()
    {
        if(!Application.isPlaying)
        {
        }
    }

    //---------------------------------------------------------
    float Get2dAngleBetweenTwoPoints(Vector2 p1, Vector2 p2)
    {
        float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;

        return angle;
    }


    //---------------------------------------------------------
    void Update()
    {
        if (!Application.isPlaying)
            return;

        //base.ManualUpdate(parentAnimProgress);

        //progress = parentAnimProgress;

        //if (isFlatLine)
        //{
        //    transform.position = StartPoint.transform.position + EndPoint.transform.position;
        //    transform.position /= 2.0f;
        //}
        //else

        //if (Application.isPlaying)
          //  return;

        cylinder.transform.position = Vector3.Lerp(StartPoint.transform.position, EndPoint.transform.position, 0.5f) ;


        cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up,
                (EndPoint.transform.position - StartPoint.transform.position).normalized);


        Vector3 inverseSP = transform.InverseTransformPoint(StartPoint.transform.position);
        Vector3 inverseEP = transform.InverseTransformPoint(EndPoint.transform.position);

        float length = Vector3.Distance(inverseSP, inverseEP) * 0.5f;// * transform.lossyScale.x;

        cylinder.transform.localScale = new Vector3(thickness, length, thickness);

        // Update line thickness to match UI_Manager settings
        float lineThickness = 1;// UI_Manager.LineThickness;
        

//            startPosition.transform.localScale = new Vector3(thickness, length * originOffset * 1.0f * (1.0f / widgetParent.worldScaleMultiplier), thickness);

        //if (isFlatLine)
        //{

        //}
        //else
        //{
  //          Vector3 startPos = new Vector3(0, (length * 1.0f) * originOffset * (1.0f / widgetParent.worldScaleMultiplier), 0);

    //        startPosition.transform.localPosition = startPos;
        //}




        // If adjusted scale is zero, shrink everything to be invisible (otherwise you have a thin floating disc at the end)
        //            startPosition.transform.localScale = new Vector3(thicknessCheck, length * AdjustedScale * 1.0f, thicknessCheck);
        //}
    }
}
