using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthBarUI : MonoBehaviour
{
    enum State{
        Detected, Close, Disappear
    }

    State state = State.Disappear;

    public Image stealthGaugeRight;
    public Image stealthGaugeLeft;

    public Image rightBG;
    public Image leftBG;

    public Image eye;
    public Image exclam;
    public Color exclamColor;

    private float fillAmount;
    private float parseFillAmount;

    private float time = 0f;
    private bool isPlayerDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetStealthGauge();
        OffStealthGauge();
        exclamColor = exclam.color;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Detected:
                Debug.Log("Detected");
                OnStealthGauge();
                if(stealthGaugeLeft.fillAmount <= 1f)
                {
                    SetStealthGauge(1f);
                }
                exclamColor.a = 1f;
                exclam.color = exclamColor;
                break;

            case State.Close:
                Debug.Log("close");
                OnStealthGauge();
                if (stealthGaugeLeft.fillAmount == 0)
                {
                    SetStealthGauge(0.6f);
                }
                else if(stealthGaugeLeft.fillAmount <= 1f){
                    parseFillAmount += this.fillAmount * Time.deltaTime;
                    float fillAmount = Mathf.Floor(parseFillAmount * 20f) / 20f;
                    stealthGaugeRight.fillAmount = fillAmount;
                    stealthGaugeLeft.fillAmount = fillAmount;
                }
                else
                {
                    isPlayerDetected = true;
                }
                break;

            case State.Disappear:
                Debug.Log("disappear");
                if(stealthGaugeLeft.fillAmount > 0.6f)
                {
                    parseFillAmount -= 0.5f * Time.deltaTime;
                    float fillAmount = Mathf.Floor(parseFillAmount * 20f) / 20f;
                    stealthGaugeLeft.fillAmount = fillAmount;
                    stealthGaugeRight.fillAmount = fillAmount;
                }
                else
                {
                    OffStealthGauge();
                    ResetStealthGauge();
                }
                break;
        }

        if(time < 0.2f)
        {
            time += Time.deltaTime;
        }
        else
        {
            state = State.Disappear;
        }
    }


    public void SetStateClose(float distToTarget)
    {
        if (!isPlayerDetected)
        {
            time = 0f;
            fillAmount = (10 - distToTarget) / 10;
            state = State.Close;
        }
        else
        {
            state = State.Detected;
        }
    }

    public void SetStateDetected()
    {
        time = 0f;
        state = State.Detected;
    }


    public void ResetStealthGauge()
    {
        stealthGaugeRight.fillAmount = 0;
        stealthGaugeLeft.fillAmount = 0;
    }

    public void SetStealthGauge(float gauge)
    {
        stealthGaugeRight.fillAmount = gauge;
        stealthGaugeLeft.fillAmount = gauge;
    }

    private void OnStealthGauge()
    {
        stealthGaugeRight.enabled = true;
        stealthGaugeLeft.enabled = true;
        rightBG.enabled = true;
        leftBG.enabled = true;
        eye.enabled = true;
        exclam.enabled = true;
        exclamColor.a = 0.1f;
        exclam.color = exclamColor;
    }

    private void OffStealthGauge()
    {
        stealthGaugeRight.enabled = false;
        stealthGaugeLeft.enabled = false;
        rightBG.enabled = false;
        leftBG.enabled = false;
        eye.enabled = false;
        exclam.enabled = false;
    }
}
