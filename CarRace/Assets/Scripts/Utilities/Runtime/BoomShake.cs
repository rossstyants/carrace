	using System;using UnityEngine;

    public class BoomShake : MonoBehaviour
    {
        public ShakeInfo shake;
        ShakeInfo rot;

	public bool shakePosition = false;
	public bool shakeRotation = false;
	public bool shakeOnAwake = false;
	
		public bool useNonProportionalScaleUpTime = false;
		public float nonProportionalScaleUpTime = 0.1f;
	
		float shakeTime;
        public float boomShakeMaxSize = 1.0f;
        public float shakeTimer;
		public float defaultTime = 1.0f;
	public float defaultMagnitudeMin = 5.0f;
	public float defaultMagnitude = 5.0f;
	public float defaultSpeedMultiply = 1.0f;	
	
		//change these to 0 for nothing on this axis
		public float xMultiplier = 1.0f;
		public float yMultiplier = 1.0f;
		public float zMultiplier = 1.0f;

	//public float defaultTimeRot = 1.0f;
	public float defaultMagnitudeRotMin = 5.0f;
	public float defaultMagnitudeRot = 5.0f;
	public float defaultSpeedMultiplyRot = 1.0f;	
	
		Vector3 shakeValue;
		Vector3 shakeValueRot;
	Vector3 originalPosition;
		
	public bool isDisabled = false;
	
        public struct ShakeInfo
		{
			public Vector3 shakeSpeed;
			public Vector3 shakePosition;
			public Vector3 shakeSize;
        };

		public void SetShake(ShakeInfo inThing) {shake = inThing;}///@property(readwrite,assign) ShakeInfo shake;
		
		public ShakeInfo GetShake() 
		{
			return shake;
		}
	
		///@property(readwrite,assign) ShakeInfo shake;

        void Awake()
        {
			originalPosition = gameObject.transform.localPosition;
			Ross_Utils.Initialise();
		}	
	
        public void ResetShake()
        {
			gameObject.transform.localPosition = originalPosition;
			shakeTimer = 0.0f;
		}
	
        void Start()
        {
            shakeTimer = 0.0f;
            shakeTime = 0.0f;
		
			if (shakeOnAwake)
			{
				this.StartShake();
			}
        }
        public void Setup(ShakeInfo info)
        {
            shake = info;
        }

        public void StartShakeWithSpeed(float time, float ofMagnitude,float speedMultiply, float rotMagnitude, float rotMultiply)
        {
			this.Reset();
		
			//could +/- 10% here
			shake.shakeSize.x = ofMagnitude * xMultiplier;
			shake.shakeSize.y = ofMagnitude * yMultiplier;
			shake.shakeSize.z = ofMagnitude * zMultiplier;
				
			shakeTime = time;
            shakeTimer = time;

			shake.shakeSpeed.x = speedMultiply * (1 + (((float)(Ross_Utils.GetRand( 400))) / 1000));
            shake.shakeSpeed.y = speedMultiply * (1 + (((float)(Ross_Utils.GetRand( 400))) / 1000));
            shake.shakeSpeed.z = speedMultiply * (1 + (((float)(Ross_Utils.GetRand( 400))) / 1000));

			//=======
		
			rot.shakeSize.x = rotMagnitude;
			rot.shakeSize.y = rotMagnitude;
			rot.shakeSize.z = rotMagnitude;
				
			rot.shakeSpeed.x = rotMultiply * (1 + (((float)(Ross_Utils.GetRand( 400))) / 1000));
            rot.shakeSpeed.y = rotMultiply * (1 + (((float)(Ross_Utils.GetRand( 400))) / 1000));
            rot.shakeSpeed.z = rotMultiply * (1 + (((float)(Ross_Utils.GetRand( 400))) / 1000));
	}

        public void StartBigShake(bool toRight)
        {
			if (toRight)
				rot.shakePosition.z = -Mathf.PI / 2.0f;
			else	
				rot.shakePosition.z = Mathf.PI / 2.0f;
		
			rot.shakeSize.z = 25.0f;
		
//			this.StartShake(defaultTime,defaultMagnitude * 5.0f);
		}


	//0-1 min-max
	public void StartShake(float ratio)
	{
		Debug.Log("BoomShake ratio=" +ratio );

		float magnitude = Ross_Utils.SetRatio(ratio, defaultMagnitudeMin, defaultMagnitude);
		float rmagnitude = Ross_Utils.SetRatio(ratio, defaultMagnitudeRotMin, defaultMagnitudeRot);

		this.StartShake(defaultTime, magnitude, rmagnitude);
	}

	public void StartShake()
        {
			this.StartShake(defaultTime,defaultMagnitude,defaultMagnitudeRot);
		}	
	
	
        public void StartShake(float time, float ofMagnitude, float rotMagnitude)
        {
			this.StartShakeWithSpeed(time,ofMagnitude,defaultSpeedMultiply,rotMagnitude,defaultSpeedMultiplyRot);
		}	

        public void StopShake(float time)
        {
			shakeTimer = 0.0f;
		}		

		public void ChangeRotationShakeSize(float newSize)
        {
			rot.shakeSize.z = newSize;	

			Vector3 rotBefore = gameObject.transform.localEulerAngles;		
			rotBefore.z = 0.0f;		
			gameObject.transform.localEulerAngles = rotBefore;  
	
	}
	
        public bool IsShaking()
        {
            return (shakeTimer > 0);
        }

        void FixedUpdate()
        {
            if (shakeTimer <= 0) return;
		
			if (isDisabled)
				return;
		
			Ross_Utils.IncrementLoop(ref shake.shakePosition.x, shake.shakeSpeed.x, -Mathf.PI,Mathf.PI);
			Ross_Utils.IncrementLoop(ref shake.shakePosition.y, shake.shakeSpeed.y, -Mathf.PI,Mathf.PI);
			Ross_Utils.IncrementLoop(ref shake.shakePosition.z, shake.shakeSpeed.z, -Mathf.PI,Mathf.PI);

            float size;
            const float kIncProportion = 0.94f;
		
		if (!useNonProportionalScaleUpTime)
		{
            if (shakeTimer >= (shakeTime * kIncProportion)) 
			{
                size = 1 - Ross_Utils.GetCosInterpolationHalf(shakeTimer, (shakeTime * kIncProportion), shakeTime);
            }
            else 
			{
                size = Ross_Utils.GetCosInterpolationHalf(shakeTimer, 0, (shakeTime * kIncProportion));
            }
		}
		else
		{
            if (shakeTimer >= (shakeTime - nonProportionalScaleUpTime)) 
			{
                size = 1 - Ross_Utils.GetCosInterpolationHalf(shakeTimer, (shakeTime - nonProportionalScaleUpTime), shakeTime);
            }
            else 
			{
                size = Ross_Utils.GetCosInterpolationHalf(shakeTimer, 0, (shakeTime - nonProportionalScaleUpTime));
            }
		}
			
            shakeValue.x = (float)Math.Cos(shake.shakePosition.x) * shake.shakeSize.x * size * boomShakeMaxSize;
            shakeValue.y = (float)Math.Cos(shake.shakePosition.y) * shake.shakeSize.y * size * boomShakeMaxSize;
            shakeValue.z = (float)Math.Cos(shake.shakePosition.z) * shake.shakeSize.z * size * boomShakeMaxSize;

			shakeTimer -= Time.deltaTime;

		if (shakePosition)
		{
			if (shakeTimer <= 0.0f)
			{
				gameObject.transform.localPosition = originalPosition;
			}
			else
			{

				Vector3 positionBeforeShake = gameObject.transform.localPosition;

				if (shake.shakeSize.x > 0.0f)
					positionBeforeShake.x += shakeValue.x;

				if (shake.shakeSize.y > 0.0f)
					positionBeforeShake.y += shakeValue.y;

				if (shake.shakeSize.z > 0.0f)
					positionBeforeShake.z += shakeValue.z;

				gameObject.transform.localPosition = positionBeforeShake;
			}
		}
			//===========================
		
			if (!shakeRotation)
				return;

			Ross_Utils.IncrementLoop(ref rot.shakePosition.z, rot.shakeSpeed.z, -Mathf.PI,Mathf.PI);

            shakeValueRot.x = (float)Math.Cos(rot.shakePosition.x) * rot.shakeSize.x * size;
            shakeValueRot.y = (float)Math.Cos(rot.shakePosition.y) * rot.shakeSize.y * size;
            shakeValueRot.z = (float)Math.Cos(rot.shakePosition.z) * rot.shakeSize.z * size * boomShakeMaxSize;
		
		if (shakeTimer <= 0.0f)
		{
			shakeValueRot.x = 0.0f;
			shakeValueRot.y = 0.0f;
			shakeValueRot.z = 0.0f;
		}


		//Vector3 rotBefore = gameObject.transform.localEulerAngles;
		
		//	rotBefore.z = shakeValueRot.z;
		
			gameObject.transform.localEulerAngles = shakeValueRot;        
		}
	
        public void Reset()
        {
            shakeTimer = 0;
            shake.shakePosition.x = Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
			shake.shakePosition.x -= Mathf.PI;
            shake.shakePosition.y = Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
			shake.shakePosition.y -= Mathf.PI;
            shake.shakePosition.z = Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
			shake.shakePosition.z -= Mathf.PI;
            shakeValue.x = 0.0f;
            shakeValue.y = 0.0f;
            shakeValue.z = 0.0f;
					
			//===========
					
			rot.shakePosition.x = Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
			rot.shakePosition.x -= Mathf.PI;
            rot.shakePosition.y = Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
			rot.shakePosition.y -= Mathf.PI;
            rot.shakePosition.z = Ross_Utils.GetRandBetween(0.0f,2.0f * Mathf.PI);
			rot.shakePosition.z -= Mathf.PI;
            shakeValueRot.x = 0.0f;
            shakeValueRot.y = 0.0f;
            shakeValueRot.z = 0.0f;
        }

    }
