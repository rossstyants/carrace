using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Ross_Utils : MonoBehaviour
{
	public static bool disableLogs = false;	
	
	static bool initialised = false;
    static float[] preCalculatedCos = new float[4096];


    //	public static void MakePixelPerfectWithSize(GameObject inSprite, int widthThreshold)
    //	{
    //		inSprite.GetComponent<UISprite>().MakePixelPerfect();
    //		
    //		if (inSprite.GetComponent<UISprite>().width < widthThreshold)
    //		{
    //			inSprite.GetComponent<UISprite>().width *= 4;
    //			inSprite.GetComponent<UISprite>().height *= 4;
    //		}
    //	}
    //
    //
    //	public static void MakePixelPerfectForMode(GameObject inSprite)
    //	{
    //		inSprite.GetComponent<UISprite>().MakePixelPerfect();
    //	
    //		if (Globals.g_mainLoop.gameMode == MainLoop.GameMode.kNightTime)
    //		{
    //			inSprite.GetComponent<UISprite>().width *= 4;
    //			inSprite.GetComponent<UISprite>().height *= 4;
    //		}
    //	}

    public static Vector3[] LineCircleIntersection(Vector3 center, float radius, Vector3 rayStart, Vector3 rayEnd)
    {
        bool logOn = false;

        if (logOn)
        {
            Debug.Log("center: " + center);
            Debug.Log("radius: " + radius);
            Debug.Log("rayStart: " + rayStart);
            Debug.Log("rayEnd: " + rayEnd);
        }

        Vector3 directionRay = rayEnd - rayStart;
        Vector3 centerToRayStart = rayStart - center;

        float a = Vector3.Dot(directionRay, directionRay);
        float b = 2 * Vector3.Dot(centerToRayStart, directionRay);
        float c = Vector3.Dot(centerToRayStart, centerToRayStart) - (radius * radius);

        float discriminant = (b * b) - (4 * a * c);
        if (discriminant >= 0)
        {
            //Ray did not miss
            discriminant = Mathf.Sqrt(discriminant);

            //How far on ray the intersections happen
            float t1 = (-b - discriminant) / (2 * a);
            float t2 = (-b + discriminant) / (2 * a);

            Vector3[] hitPoints = null;

            if (t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1)
            {
                //total intersection, return both points
                hitPoints = new Vector3[2];
                hitPoints[0] = rayStart + (directionRay * t1);
                hitPoints[1] = rayStart + (directionRay * t2);
            }
            else
            {
                //Only one intersected, return one point
                if (t1 >= 0 && t1 <= 1)
                {
                    hitPoints = new Vector3[1];
                    hitPoints[0] = rayStart + (directionRay * t1);
                }
                else if (t2 >= 0 && t2 <= 1)
                {
                    hitPoints = new Vector3[1];
                    hitPoints[0] = rayStart + (directionRay * t2);
                }
            }
            return hitPoints;
        }
        //No hits
        return null;
    }

    public static Vector2 GetVectorFromAngle(float inAngle)
    {
        Vector2 outVector = new Vector2((float)Math.Sin(inAngle), (float)Math.Cos(inAngle));

		outVector.y = -outVector.y;
		
		return outVector;
    }
	
	public static void SetLocalPositionY(ref GameObject inObj, float inVal)
	{
		Vector3 posNow = inObj.transform.localPosition;
		posNow.y = inVal;
		inObj.transform.localPosition = posNow;
	}
	
	public static void AddToLocalPositionY(ref GameObject inObj, float inVal)
	{
		Vector3 posNow = inObj.transform.localPosition;
		posNow.y += inVal;
		inObj.transform.localPosition = posNow;
	}
	
	public static void AddToLocalPosition(GameObject inObj, Vector3 inVal)
	{
		Vector3 posNow = inObj.transform.localPosition;
		posNow += inVal;
		inObj.transform.localPosition = posNow;
	}

	public static void AddToRotation(GameObject inObj, Vector3 inVal)
	{
		Vector3 posNow = inObj.transform.localEulerAngles;
		posNow += inVal;
		inObj.transform.localEulerAngles = posNow;
	}
	public static void AddToRotation(ref Vector3 inVec, Vector3 inVal)
	{
	}
	
	public static void Log(string message, Vector2 vec) 
	{
		string outString = message + " x:" + vec.x.ToString() + " y:" + vec.y.ToString();

		Log(outString);
	}
	public static void Log(string message, Vector3 vec) 
	{
		string outString = message + " x:" + vec.x.ToString() + " y:" + vec.y.ToString() + " z:" + vec.z.ToString();

		Log(outString);
	}
	
	public static void Log(object message) 
	{
#if UNITY_EDITOR
		if (!disableLogs)
		{
			Debug.Log (message);	
		}

#endif

	}
		
	public static void LogMaker(object message) 
	{
#if UNITY_EDITOR
		if (!disableLogs)
		{
			Debug.Log (message);	
		}
#endif
	}

	public static void LogAudio(object message) 
	{
#if UNITY_EDITOR
		if (!disableLogs)
		{
			Debug.Log (message);	
		}
#endif
	}
		
	//scales down if too big
	//-----------------------------------------------------------------------------
    public static void AdjustScaleToMaximum(Vector2 maxSize, GameObject inObject)
	{
		if (maxSize.y > 0.0f)
		{
			if (inObject.transform.localScale.y > maxSize.y)
			{
				float ratio = maxSize.y / inObject.transform.localScale.y;
				
				Vector3 scaleNow = inObject.transform.localScale;
				scaleNow.x *= ratio;
				scaleNow.y = maxSize.y;
				inObject.transform.localScale = scaleNow;			
			}
		}
		
		if (maxSize.x > 0.0f)
		{
			if (inObject.transform.localScale.x > maxSize.x)
			{
				float ratio = maxSize.x / inObject.transform.localScale.y;
				
				Vector3 scaleNow = inObject.transform.localScale;
				scaleNow.y *= ratio;
				scaleNow.x = maxSize.x;
				inObject.transform.localScale = scaleNow;			
			}
		}
	}
	
	//changes scale to fit these proportions whatever it was before
	//-----------------------------------------------------------------------------
    public static void AdjustScaleToProportions(Vector2 maxSize, GameObject inObject)
	{
		
		float xRatio = maxSize.x / inObject.transform.localScale.x;
		float yRatio = maxSize.y / inObject.transform.localScale.y;
		
		
		if (xRatio > yRatio)
		{
			float ratio = maxSize.y / inObject.transform.localScale.y;
			
			Vector3 scaleNow = inObject.transform.localScale;
			scaleNow.x *= ratio;
			scaleNow.y = maxSize.y;
			inObject.transform.localScale = scaleNow;			
		}
		else		
		{
			float ratio = maxSize.x / inObject.transform.localScale.x;
			
			Vector3 scaleNow = inObject.transform.localScale;
			scaleNow.y *= ratio;
			scaleNow.x = maxSize.x;
			inObject.transform.localScale = scaleNow;			
		}
	}
	
	//-----------------------------------------------------------------------------
    public static Vector3 GetPositionBetween(float ratio, Vector3 point1, Vector3 point2)
    {
				Vector3 difference = new Vector3(point2.x - point1.x, point2.y - point1.y, point2.z - point1.z);
        difference.x *= ratio;
				difference.y *= ratio;
				difference.z *= ratio;
				Vector3 outPoint = new Vector3(point1.x + difference.x, point1.y + difference.y, point1.z + difference.z);
        return outPoint;
    }
	

	//-----------------------------------------------------------------------------
    public static float GetRatio(float inVal, float inMin, float inMax)
    {
        if (inVal < inMin) inVal = inMin;

        if (inVal > inMax) inVal = inMax;

        return 1.0f - ((inMax - inVal) / (inMax - inMin));
    }

	//-----------------------------------------------------------------------------
	public static void Limit(ref float inVal, float min, float max)
    {			
		if (inVal < min)
			inVal = min;
		else if (inVal > max)
			inVal = max;
	}

	//-----------------------------------------------------------------------------
	public static void DecrementLoop(ref int inVal, int min, int max)
    {			
		inVal--;
		if (inVal < min)
		{
			inVal = max;
		}
	}

	//-----------------------------------------------------------------------------
	public static void IncrementLoop(ref int inVal, int min, int max)
    {			
		inVal++;
		if (inVal > max)
		{
			inVal = min;
		}
	}

	//-----------------------------------------------------------------------------
/*	public static void MakePixelPerfect(ref UISprite sprite)
	{
		//calls the UISPrite MakePixelPerfect func but also adjusts this for retina screen etc...
		
		sprite.MakePixelPerfect();
		
		//should really check here if this specific atlas is high res in this game
		if (GameSettings.guiResolution == GameSettings.GuiResolution.kHighRes_Full)
		{
			Vector3 scale = sprite.cachedTransform.localScale;
			scale.x *= 0.5f;
			scale.y *= 0.5f;
			sprite.cachedTransform.localScale = scale;
		}
	}*/
	
	//-----------------------------------------------------------------------------
	public static void MakeRotationSensible(ref float inRot)
    {			
		while (inRot > 360.0f)
		{
			inRot -= 360.0f;
		}
		
		while (inRot < 0.0f)
		{
			inRot += 360.0f;
		}
	}
	
	
	//-----------------------------------------------------------------------------
	public static void Assert(bool inThing)
    {			
#if UNITY_EDITOR
		if (!inThing) 
		{
			throw new Exception();
		}
#endif
	}

	//-----------------------------------------------------------------------------
	public static void LagTo(ref float inValue, float targetValue, float lagVal)
    {			
		inValue += ((targetValue - inValue) * lagVal);
	}
	
	//-----------------------------------------------------------------------------
	public static void LagTo(ref Vector2 inValue, Vector2 targetValue, float lagVal)
    {			
		LagTo(ref inValue.x,targetValue.x,lagVal);
		LagTo(ref inValue.y,targetValue.y,lagVal);
	}		
	//-----------------------------------------------------------------------------
	public static void LagTo(ref Vector3 inValue, Vector3 targetValue, float lagVal)
    {			
		LagTo(ref inValue.x,targetValue.x,lagVal);
		LagTo(ref inValue.y,targetValue.y,lagVal);
		LagTo(ref inValue.z,targetValue.z,lagVal);
	}	
	
	//-----------------------------------------------------------------------------
    public static float SetRatio(float inVal, float inMin, float inMax)
    {
        float ratio = inVal;
        if (inVal < 0) ratio = 0;

        if (inVal > 1.0f) ratio = 1.0f;

        return inMin + ((inMax - inMin) * ratio);
    }
	
	//-----------------------------------------------------------------------------
/*	public static void SetUISpriteToSize(GameObject inObj, DataModelStockManager.IngredientName inType)
	{
		Ross_Utils.Log ("*******  *******  *******  *******  *******  *******  *******  SetUISpriteToSize " + inType.ToString());
		
		UISprite uisprite = (UISprite)inObj.GetComponent(typeof(UISprite));
			
		//uisprite.MakePixelPerfect();
		
		Vector3 newPos = inObj.transform.localScale;
		
		newPos.x = 95.0f;
		newPos.y = 95.0f;		
		
		switch(inType)
		{
		case MakerKnowledge.IngredientType.kCustomIceCream_SyrupStrawberry:
		case MakerKnowledge.IngredientType.kCustomIceCream_Syrup:
			
			newPos.x = 93.0f;
			newPos.y = 246.0f;
		
			break;
		case MakerKnowledge.IngredientType.kCustomIceCream_ChocolateSprinkles:
		case MakerKnowledge.IngredientType.kCustomIceCream_Nuts:
		case MakerKnowledge.IngredientType.kCustomIceCream_HundredsAndThousands:
			
			newPos.x = 176.0f * 0.75f;
			newPos.y = 206.0f * 0.75f;
		
			break;
		}
		
		newPos.x *= 0.8f;
		newPos.y *= 0.8f;
		
//		newPos.x = 176.0f;
//		newPos.y = 206.0f;

		
		inObj.transform.localScale = newPos;
	}*/
	
	//-----------------------------------------------------------------------------
	public static string[] DivideStringIntoSentences(string inLongString)
	{
		//simple way for now
		string[] outSentences = inLongString.Split('*');
		
		return outSentences;
	}

	//-----------------------------------------------------------------------------
    public static bool IsWithinCircle(Vector2 inPoint, Vector2 centre, float radius)
    {
		float sqrRadius = radius * radius;
		
		Vector2 diffVec = inPoint - centre;
		
		return (diffVec.SqrMagnitude() <= sqrRadius);
	}
	
	//-----------------------------------------------------------------------------
    public static bool IsWithinRectangle(Vector2 inPoint, Vector2 rectCentre, float width, float height)
    {
        return ((inPoint.x < (rectCentre.x + (width / 2))) && (inPoint.x > (rectCentre.x - (width / 2))) && (inPoint.y < (rectCentre.y + (height / 2))) && (
          inPoint.y > (rectCentre.y - (height / 2))));
    }
	
	//-----------------------------------------------------------------------------
	public static string GetPoundsStringFromPence(int inPence, bool useNumbersWhenZero = false)
	{
		string costString = "£";
		
		if (inPence == 0)
		{
//            if (useNumbersWhenZero) 
            costString = "@0.00";
//			else costString = "!";
		}
		else if (inPence < 100)
		{
			costString = inPence.ToString() + "p";
		}
        else if (inPence >= 10000) // over £100 then lose the pence
        {
            int totUpPounds = (inPence / 100);
            costString = "@" + totUpPounds.ToString();
        }
		else
		{
			int totUpPounds = (inPence / 100);
		
			int totUpLeftoverPence = inPence%100;
			
			if (totUpLeftoverPence < 10)
				costString = "@" + totUpPounds.ToString() + ".0" + totUpLeftoverPence.ToString();
			else
				costString = "@" + totUpPounds.ToString() + "." + totUpLeftoverPence.ToString();
				
		}	
		
		return costString;
	}
	
    public static bool IsBetween(int inV, int minV, int maxV)
    {
        return ((inV >= minV) && (inV <= maxV));
    }
    public static bool IsBetweenFloat(float inV, float minV, float maxV)
    {
        return ((inV >= minV) && (inV <= maxV));
    }
	
	
	public static Vector3 GetWorldScale(Transform transform)
    {
        Vector3 worldScale = transform.localScale;

        Transform parent = transform.parent;

        while (parent != null)
        {
            worldScale = Vector3.Scale(worldScale,parent.localScale);

            parent = parent.parent;
        }

        return worldScale;
    }	

	public static double AngleDifference( double angle1, double angle2 )
	{
		double diff = ( angle2 - angle1 + 180 ) % 360 - 180;
		return diff < -180 ? diff + 360 : diff;
	}

	//cubeSize is x,y,z scale
	public static Vector3 GetPositionWithinCube(Vector3 center, Vector3 cubeSize)
	{
		float x = UnityEngine.Random.Range(-cubeSize.x*0.5f,cubeSize.x*0.5f);
		float y = UnityEngine.Random.Range(-cubeSize.y*0.5f,cubeSize.y*0.5f);
		float z = UnityEngine.Random.Range(-cubeSize.z*0.5f,cubeSize.z*0.5f);

		//Debug.Log("z " + z);

		return center + new Vector3(x,y,z);
	}


	public static Vector2 GetPositionWithinCircle(float circleRadius)
	{
		float distThing = 1000000000.0f;
		float sqrThing = circleRadius * circleRadius;
		float xPos;
		float yPos;

		do{
			xPos = Ross_Utils.GetRandBetween(-circleRadius,circleRadius);	
			yPos = Ross_Utils.GetRandBetween(-circleRadius,circleRadius);	
		
			distThing = Ross_Utils.GetSqrDistance(new Vector2(0,0),new Vector2(xPos,yPos));

		}while(distThing > sqrThing);

		return new Vector2(xPos,yPos);
	}
	
	public static float GetRandomRotation()
    {
		return Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
	}	
	
   	public static float GetRandBetween(float inValMin, float inValMax)
    {
        int min = (int) (inValMin * 10000.0f);
        int max = (int) (inValMax * 10000.0f);
        int outInt = min + (Ross_Utils.GetRand( (max - min)));
        return ((float) outInt) / 10000.0f;
    }	
	
	//hmm despite docs - max seems to be EXclusive not inclusive
	public static int GetRand(int inMaxExclusive)
    {
		return (int)(UnityEngine.Random.Range(0,inMaxExclusive));
	}

	//converts to LOCAL coords
	//-----------------------------------------------------------------------------
	public static Vector2 ConvertTouchPositionToObjectCoords(Vector2 inTouchPos)
	{
		const float kOrthoHeight = 768.0f;

		inTouchPos.x -= Screen.width / 2.0f;
		inTouchPos.y -= Screen.height / 2.0f;
		
		float ratio = kOrthoHeight / Screen.height;
		
		Vector2 outPos = new Vector2(inTouchPos.x * ratio, inTouchPos.y * ratio);
		
		return outPos;
	}	
	
	//-----------------------------------------------------------------------------
	public static void ConvertWorldPositionToPixels(ref Vector2 inTouchPos)
	{
		const float kOrthoHeight = 768.0f;		

		inTouchPos.x *= (kOrthoHeight / 2.0f);
		inTouchPos.y *= (kOrthoHeight / 2.0f);
	}	
	
	//-----------------------------------------------------------------------------
	public static void ConvertToWorldPosition(ref Vector2 inPos)
	{
		const float kOrthoHeight = 768.0f;		

		inPos.x /= (kOrthoHeight / 2.0f);
		inPos.y /= (kOrthoHeight / 2.0f);

//		return inPos;
	}
	public static Vector2 ConvertToWorldPosition(Vector2 inPos)
	{
		Vector2 outVec = new Vector2(inPos.x / 378.0f, inPos.y / 378.0f);

		return outVec;
	}
	
	public static float GetSqrDistance(Vector2 point1, Vector2 point2)
    {
        float xDist = point2.x - point1.x;
        float yDist = point2.y - point1.y;
        float sqrDistance = ((xDist * xDist) + (yDist * yDist));
        return sqrDistance;
    }	
	public static float GetDistance(Vector2 point1, Vector2 point2)
    {
		return Mathf.Sqrt(GetSqrDistance(point1,point2));
	}	
			
	public static int GetNearestThing(Vector2 headPos, Vector2[] positionArray, int numItems)
	{
		float nearestDistanceSqr = -1.0f;
		int nearestItem = -1;
		
		for (int i = 0; i < numItems; i++)
		{
			float sqrDist = Ross_Utils.GetSqrDistance(headPos,positionArray[i]);
			
			if ((nearestItem == -1) || (sqrDist < nearestDistanceSqr))
			{
				nearestItem = i;
				nearestDistanceSqr = sqrDist;
			}
		}
		
		return nearestItem;
	}	
	
 	public static bool Approach(ref float inVal, float inTarget, float inApproachSpeed)
    {
        Ross_Utils.Assert(inApproachSpeed >= 0);
		
        if (inVal < inTarget) 
		{
            inVal += inApproachSpeed;
            if ((inVal) >= inTarget)
			{
				inVal = inTarget;
				return true;
			}
        }
        else if (inVal > inTarget) 
		{
            inVal -= inApproachSpeed;
            if ((inVal) <= inTarget) 
			{
				inVal = inTarget;
				return true;
			}
        }

        return false;
    }
	
	public static void Initialise()
    {
        const int kNumDiscreteBlobs = 4096;
        float thing = (2.0f * Mathf.PI) / ((float)(kNumDiscreteBlobs - 1));
        float cosPos = -Mathf.PI;
        for (int i = 0; i < kNumDiscreteBlobs; i++) {
            preCalculatedCos[i] = (float)Math.Cos(cosPos);
            preCalculatedCos[i] += 1.0f;
            preCalculatedCos[i] *= 0.5f;
            cosPos += thing;
        }
		initialised = true;
    }	
	
    public static float GetCosInterpolationQuarter(float inVal, float inMin, float inMax)
    {
        if (inVal < inMin) inVal = inMin;
        if (inVal > inMax) inVal = inMax;

        const float kQuarterThing = 0.147445394f;
        float ratio = 1.0f - ((inMax - inVal) / (inMax - inMin));
        float cosRatio = Mathf.PI + (ratio * (Mathf.PI / 4.0f));
        float outVal = (float)Math.Cos(cosRatio);
        outVal += 1.0f;
        outVal /= 2.0f;
        float whatTheHeckAmIDoing = outVal / kQuarterThing;
        return whatTheHeckAmIDoing;
    }	
	
    public static float GetCosInterpolation(float inVal, float inMin, float inMax)
    {
		if (inVal < inMin) inVal = inMin;

        if (inVal > inMax) inVal = inMax;

		float range = inMax - inMin;
		
		Ross_Utils.Assert(range != 0.0f);
	
        float ratio = (inMax - inVal) / (inMax - inMin);
        ratio *= 4095.999f;
				
		Ross_Utils.Assert(initialised);
	
        return preCalculatedCos[(int) ratio];
    }	
	
	public static float GetCosInterpolationHalf(float inVal, float inMin, float inMax)
    {
//        const float kPi = Mathf.PI;
		
        if (inVal < inMin) inVal = inMin;

        if (inVal > inMax) inVal = inMax;

        float ratio = (inMax - inVal) / (inMax - inMin);
        float cosRatio = (ratio * Mathf.PI);
        float outVal = (float)Math.Cos(cosRatio);
        outVal += 1.0f;
        outVal /= 2.0f;
        return outVal;
    }	
	
    public static float GetAngleFromXY(Vector2 inVec)
    {
		//Debug.Log("inVec.x" + inVec.x);
		//Debug.Log("inVec.y" + inVec.y);

		return Mathf.Atan2(inVec.x,inVec.y);

  //      float angle = Mathf.Abs(inVec.x) / Mathf.Abs(inVec.y);

		//float outAngle = (float)Math.Atan(angle);
        
		//if (inVec.x > 0.0f) {
  //          if (inVec.y > 0.0f) {
  //              outAngle = Mathf.PI - outAngle;
  //          }

  //      }
  //      else {
  //          if (inVec.y > 0.0f) {
  //              outAngle = Mathf.PI + outAngle;
  //          }
  //          else {
  //              outAngle = (2.0f * Mathf.PI) - outAngle;
  //          }

  //      }

  //      return outAngle;
    }	
	
	public static void IncrementLoop(ref float inVal, float addAmount, float loopMin, float loopMax)
    {
        inVal += addAmount;
        if (inVal > loopMax) 
		{
			float range = loopMax - loopMin;
            inVal -= range;
        }
		else if(inVal < loopMin)
		{
			float range = loopMax - loopMin;
            inVal += range;
		}
    }
    public static Color LerpHSV(Color a, Color b, float x)
    {
        Vector3 ah = RGB2HSV(a);
        Vector3 bh = RGB2HSV(b);
        return new Color(
            Mathf.LerpAngle(ah.x, bh.x, x),
            Mathf.Lerp(ah.y, bh.y, x),
            Mathf.Lerp(ah.z, bh.z, x)
        );
    }

    static Vector3 RGB2HSV(Color color)
    {
        float cmax = Mathf.Max(color.r, color.g, color.b);
        float cmin = Mathf.Min(color.r, color.g, color.b);
        float delta = cmax - cmin;

        float hue = 0;
        float saturation = 0;

        if (cmax == color.r)
        {
            hue = 60 * (((color.g - color.b) / delta) % 6);
        }
        else if (cmax == color.g)
        {
            hue = 60 * ((color.b - color.r) / delta + 2);
        }
        else if (cmax == color.b)
        {
            hue = 60 * ((color.r - color.g) / delta + 4);
        }
        if (cmax > 0)
        {
            saturation = delta / cmax;
        }

        return new Vector3(hue, saturation, cmax);
    }

    static Color HSV2RGB(Vector3 color)
    {
        float hue = color.x;
        float c = color.z * color.y;
        float x = c * (1 - Mathf.Abs((hue / 60) % 2 - 1));
        float m = color.z - c;

        float r = 0;
        float g = 0;
        float b = 0;

        if (hue < 60)
        {
            r = c;
            g = x;
        }
        else if (hue < 120)
        {
            r = x;
            g = c;
        }
        else if (hue < 180)
        {
            g = c;
            b = x;
        }
        else if (hue < 240)
        {
            g = x;
            b = c;
        }
        else if (hue < 300)
        {
            r = x;
            b = c;
        }
        else
        {
            r = c;
            b = x;
        }

        return new Color(r + m, g + m, b + m);
    }

    public static Color LerpViaHSB(Color a, Color b, float t)
    {
        return HSBColor.Lerp(HSBColor.FromColor(a), HSBColor.FromColor(b), t).ToColor();
    }
}
