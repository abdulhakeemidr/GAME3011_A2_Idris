using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    Text resultTxt;
    Text FeedbackTxt;
    Text TimerTxt;
    Text HintTxt;

    void Awake() 
    {
        instance = this;

        Text[] allchildren = GetComponentsInChildren<Text>(true);

        foreach(Text t in allchildren)
        {
            if(t.gameObject.name == "ResultTxt")
            {
                resultTxt = t.gameObject.GetComponent<Text>();
            }
            else if(t.gameObject.name == "FeedbackTxt")
            {
                FeedbackTxt = t.gameObject.GetComponent<Text>();
            }
            else if(t.gameObject.name == "TimerTxt")
            {
                TimerTxt = t.gameObject.GetComponent<Text>();
            }
            else if(t.gameObject.name == "HintTxt")
            {
                HintTxt = t.gameObject.GetComponent<Text>();
            }
        }
    }

    void Start()
    {
        resultTxt.gameObject.SetActive(false);
        HintTxt.text = "Click on your number to guess combination number";
    }

    public void UpdateTimerText(int time)
    {
        TimerTxt.text = time.ToString();
    }

    public void UpdateFeedbackText(string message)
    {
        FeedbackTxt.text = message;
    }

    public void UpdateHintText(string message)
    {
        HintTxt.text = message;
    }

    public void ShowResultText()
    {
        resultTxt.gameObject.SetActive(true);
    }
}
