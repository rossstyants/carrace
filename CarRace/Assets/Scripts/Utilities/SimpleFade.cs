using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//{
public class SimpleFade : MonoBehaviour
{
    public enum State
    {
        Gone,
        Showing,
        Shown,
        Hiding,
    }

    public State startState;
    public State state;

    public float duration = 1.0f;
    float timer = 0.0f;
    public float minAlpha = 0.0f;
    public float maxAlpha = 1.0f;

    public Material useMaterial;
    public CanvasGroup useCanvasGroup;
    public Light useLight;
    public Image useImage;

    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;

    public bool testShow, testHide;
    public Color startColour;

    public bool setMaxAlphaAtStart;
    public bool controlGameObjectActivation;

    bool startCalled;
    private Action<int> OnShown;

    // Start is called before the first frame update
    void Start()
    {
        if (startCalled)
            return;

        startCalled = true;

        state = startState;

        if (useLight)
            startColour = useLight.color;

        if (useImage)
            startColour = useImage.color;

        if (setMaxAlphaAtStart)
        {
            if (useImage)
                maxAlpha = startColour.a;

        }

        switch (startState)
        {
            case State.Gone:
                this.HideNow();
                break;

            case State.Showing:
                this.HideNow();
                this.Show(null);
                break;
            case State.Shown:
                this.ShowNow();
                break;

            case State.Hiding:
                this.ShowNow();
                this.Hide();
                break;

        }
    }

    //-------------------------------------------------------------
    public void HideNow()
    {
        if (!startCalled)
            this.Start();

        if (controlGameObjectActivation)
            useCanvasGroup.gameObject.SetActive(false);

        SetAlpha(0.0f);

        if (useLight)
        {
            useLight.gameObject.SetActive(false);
        }

        if (useImage)
            useImage.gameObject.SetActive(false);

        state = State.Gone;

    }
    //-------------------------------------------------------------
    public void Show(Action<int> shownFunc)
    {
        OnShown = shownFunc;

        if (!startCalled)
            this.Start();

        if (useCanvasGroup != null)
        {
            useCanvasGroup.gameObject.SetActive(true);
        }

        timer = 0.0f;

        if (useLight)
        {
            useLight.gameObject.SetActive(true);
        }
        if (useImage)
            useImage.gameObject.SetActive(true);

        state = State.Showing;
    }
    //-------------------------------------------------------------
    void ShowNow()
    {
        if (!startCalled)
            this.Start();

        if (useLight)
        {
            useLight.gameObject.SetActive(true);
        }
        if (useImage)
            useImage.gameObject.SetActive(true);


        this.SetAlpha(1.0f);
    }
    //-------------------------------------------------------------
    public void Hide()
    {
        if (!startCalled)
            this.Start();

        if (state == State.Gone)
            return;

        timer = 0.0f;
        state = State.Hiding;
    }



    // Update is called once per frame
    void Update()
    {
        if (testShow)
        {
            testShow = false;
            this.Show(null);
        }
        if (testHide)
        {
            testHide = false;
            this.Hide();
        }

        switch (state)
        {
            case State.Showing:
                timer += Time.deltaTime;
                if (timer >= duration)
                {
                    state = State.Shown;
                    this.SetAlpha(1.0f);
                    OnShown?.Invoke(1);
                }
                else
                {
                    this.SetAlpha(showCurve.Evaluate(timer / duration));
                }
                break;
            case State.Hiding:
                timer += Time.deltaTime;
                if (timer >= duration)
                {
                    state = State.Gone;
                    this.HideNow();
                }
                else
                {
                    this.SetAlpha(1.0f - (showCurve.Evaluate(timer / duration)));
                }
                break;

        }
    }

    //-------------------------------------------------------------
    void SetAlpha(float inVal)
    {
        //0-1

        float alphaVal = minAlpha + ((maxAlpha - minAlpha) * inVal);

        if (useCanvasGroup != null)
        {
            useCanvasGroup.alpha = alphaVal;
            return;
        }

        if (useLight)
        {
            Color c = Color.Lerp(Color.black, startColour, alphaVal);
            //            c.a = alphaVal;
            useLight.color = c;
        }

        if (useImage)
        {
            Color c = useImage.color;
            c.a = alphaVal;
            useImage.color = c;
        }
    }
}
