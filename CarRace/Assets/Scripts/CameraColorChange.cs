using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorChange : MonoBehaviour
{
    [SerializeField] private float backColorLerp;
    public CarsRoulette CarsRoulette;
    public Color[] colors;
    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine("ColorChange");
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
//        Camera.main.backgroundColor = Ross_Utils.LerpHSV(Camera.main.backgroundColor, CarsRoulette.targetColor, backColorLerp);
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, CarsRoulette.targetColor, backColorLerp);
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
