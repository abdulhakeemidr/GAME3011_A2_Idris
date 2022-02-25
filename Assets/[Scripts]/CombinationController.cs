using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CombinationController : MonoBehaviour
{
    // the min/max values available in combination
    const int MIN = 0;
    const int MAX = 9;
    
    
    [SerializeField]
    GameObject upArrowBttn;
    [SerializeField]
    GameObject downArrowBttn;
    [SerializeField]
    GameObject combinationText;
    [SerializeField]
    Image combinationImg;

    public delegate void GuessAction();
    //public static event GuessAction onCorrectGuess;
    public UnityEvent onCorrectGuess;
    [SerializeField]
    int guessedValue = MIN;
    [SerializeField]
    int randomizedValue = MIN;


    void Start()
    {
        combinationImg = GetComponent<Image>();

        randomizedValue = Random.Range(MIN + 1, MAX + 1);
        Transform[] allchildren = GetComponentsInChildren<Transform>();

        foreach(Transform t in allchildren)
        {
            if(t.gameObject.name == "UpArrow")
            {
                upArrowBttn = t.gameObject;
            }
            else if(t.gameObject.name == "DownArrow")
            {
                downArrowBttn = t.gameObject;
            }
            else if(t.gameObject.name == "CombinationTxt")
            {
                combinationText = t.gameObject;
            }
        }

        upArrowBttn.GetComponent<Button>().onClick.AddListener(UpArrowPress);
        downArrowBttn.GetComponent<Button>().onClick.AddListener(DownArrowPress);
    }

    void Update()
    {
        combinationText.GetComponent<Text>().text = guessedValue.ToString();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            onValueComparison();
        }
    }

    void onValueComparison()
    {
        // get the distance away from it in percentages
        Debug.Log(Mathf.InverseLerp(MIN, MAX, guessedValue));

        var guessValPercent = Mathf.InverseLerp(MIN, MAX, guessedValue);
        var correctValPercent = Mathf.InverseLerp(MIN, MAX, randomizedValue);

        float colorChange = Mathf.Abs(guessValPercent - correctValPercent);
        
        var originalColor = combinationImg.color;
        Color finalColor = new Color(colorChange, originalColor.g, colorChange, originalColor.a);
        combinationImg.color = finalColor;
        
        if(guessedValue == randomizedValue)
        {
            this.gameObject.SetActive(false);
            if(onCorrectGuess != null)
            {
                //onCorrectGuess();
                //onCorrectGuess.Invoke();
            }
        }
    }

    void UpArrowPress()
    {
        guessedValue++;
        if(guessedValue > MAX)
        {
            guessedValue = MIN;
        }
    }

    void DownArrowPress()
    {
        guessedValue--;
        if(guessedValue < MIN)
        {
            guessedValue = MAX;
        }
    }
}