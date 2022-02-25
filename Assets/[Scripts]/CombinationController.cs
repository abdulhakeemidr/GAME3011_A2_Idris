using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CombinationController : MonoBehaviour
{
    [SerializeField]
    GameObject upArrowBttn;
    [SerializeField]
    GameObject downArrowBttn;
    [SerializeField]
    GameObject combinationText;

    public delegate void GuessAction();
    //public static event GuessAction onCorrectGuess;
    public UnityEvent onCorrectGuess;
    [SerializeField]
    int guessedValue = 0;
    [SerializeField]
    int randomizedValue = 0;

    void Start()
    {
        randomizedValue = Random.Range(1, 10);
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
    }

    void UpArrowPress()
    {
        guessedValue++;
        if(guessedValue > 9)
        {
            guessedValue = 0;
        }
    }

    void DownArrowPress()
    {
        guessedValue--;
        if(guessedValue < 0)
        {
            guessedValue = 9;
        }
    }
}