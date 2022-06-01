using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFade : MonoBehaviour {

    public enum State
    {
        Hidden,
        Showing,
        Shown,
        Hiding
    }

    public State state;

    public AnimationCurve fadeCurve;
    public float fadeDuration;
    float fadeTimer;

    public float maxAlpha = 1.0f;

    public bool testShow, testHide;

	//-------------------------------------------------------------
	void Start () {

        state = State.Hidden;
        this.SetAlpha(0.0f);   		
	}

    //-------------------------------------------------------------
    public void Show()
    {
        fadeTimer = fadeDuration;
        state = State.Showing;
    }

    //-------------------------------------------------------------
    public void Hide()
    {
        fadeTimer = fadeDuration;
        state = State.Hiding;
    }

    //-------------------------------------------------------------
    void SetAlpha(float inVal)
    {
        //0-1

        if (inVal <= 0.0f)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;

            Color col = GetComponent<Renderer>().sharedMaterial.GetColor("_Color");
            col.a = inVal * maxAlpha;
            GetComponent<Renderer>().sharedMaterial.SetColor("_Color", col);
        }
    }

    //-------------------------------------------------------------
    void Update()
    {
        if (testShow)
        {
            testShow = false;
            this.Show();
        }
        if (testHide)
        {
            testHide = false;
            this.Hide();
        }

        if (fadeTimer > 0.0f)
        {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer <= 0.0f)
            {
                //fade is done...
                if (state == State.Showing)
                {
                    this.SetAlpha(1.0f);
                    state = State.Shown;
                }
                else
                {
                    this.SetAlpha(0.0f);
                    state = State.Hidden;
                }
            }
            else
            {
                float fadeRatio = fadeTimer / fadeDuration;

                if (state == State.Showing)
                {
                    this.SetAlpha(1.0f - fadeRatio);
                }
                else
                {
                    this.SetAlpha(fadeRatio);
                }
            }
        }
    }
}
