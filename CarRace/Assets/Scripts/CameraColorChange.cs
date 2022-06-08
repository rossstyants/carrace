using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorChange : MonoBehaviour
{
    [SerializeField] private float colourChangeDuration;
    [SerializeField] private AnimationCurve colChangeCurve;
    private float colChangeTimer;
    [SerializeField] private float backColorLerp;
    public CarsRoulette CarsRoulette;
    public Color[] colors;
    Color targetColor;
    Color startColor;
    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine("ColorChange");
    //}

    // Update is called once per frame
    void Update()
    {
        if (colChangeTimer > 0f)
        {
            colChangeTimer -= Time.deltaTime;
            float ratio = 0f;
            if (colChangeTimer > 0f)
            {
                ratio = colChangeTimer / colourChangeDuration;
            }
                        Camera.main.backgroundColor = Color.Lerp(startColor, targetColor, 1f - ratio);
            //Camera.main.backgroundColor = Ross_Utils.LerpHSV(startColor, targetColor, 1f - ratio);
            //Camera.main.backgroundColor = Ross_Utils.LerpViaHSB(startColor, targetColor, 1f - ratio);
        }
    }
    public void ChangeBackColour(Color col)
    {
        startColor = Camera.main.backgroundColor;
        targetColor = col;
        colChangeTimer = colourChangeDuration;
    }

    private void FixedUpdate()
    {
//        Camera.main.backgroundColor = Ross_Utils.LerpHSV(Camera.main.backgroundColor, CarsRoulette.targetColor, backColorLerp);
        //Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, CarsRoulette.targetColor, backColorLerp);
    }
    //IEnumerator ColorChange()
    //{
    //    while (true)
    //    {
    //        int randColor = Random.Range(0,6);
    //        Camera.main.backgroundColor = colors[randColor];
    //        yield return new WaitForSeconds(10f);
    //    }
    //}
}
