using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

    public Image bar;
    public float maxSize;

    bool startcalled;

	//-----------------------------------
	void Start () {
        if (startcalled)
            return;

        maxSize = bar.rectTransform.sizeDelta.x;
        startcalled = true;
//        this.SetFillRatio(0.5f);
    }

    //-----------------------------------
    public void SetFillRatio(float inRatio)
    {
        if (!startcalled)
            this.Start();

        Vector2 size = bar.rectTransform.sizeDelta;
        size.x = maxSize * inRatio;
        bar.rectTransform.sizeDelta = size;
    }
}
