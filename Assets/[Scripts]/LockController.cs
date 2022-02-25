using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockController : MonoBehaviour
{
    ContentSizeFitter sizeFitter;
    public Difficulty difficultyState = Difficulty.EASY;
    GameObject parent;

    [SerializeField]
    GameObject combinationPrefab;

    [SerializeField]
    int timeTillComboReset = 10;
    float timeCounter;
    [SerializeField]
    int SECOND;


    [SerializeField]
    List<CombinationController> combinations;


    int numCombinations;
    int currentCombinationIndex = 0;

    void Awake()
    {
        parent = transform.parent.gameObject;
        //CombinationController.onCorrectGuess.AddListener(onCombinationUnlock);
        //CombinationController.onCorrectGuess += onCombinationUnlock;
    }

    void Start()
    {
        sizeFitter = GetComponent<ContentSizeFitter>();

        DifficultySetting();
        SECOND = timeTillComboReset;

        for(int i = 0; i < numCombinations; i++)
        {
            GameObject newObj = Instantiate(combinationPrefab, this.transform);
            var newCombination = newObj.GetComponent<CombinationController>();
            newCombination.combinationTxtBttn.onClick.AddListener(onCombinationUnlock);
            combinations.Add(newCombination);
        }

        var currentCombination = combinations[currentCombinationIndex];
        currentCombination.isCurrent = true;
        // resets new current combination to white
        currentCombination.combinationImg.color = Color.white;

        currentCombination.combinationText.color = Color.red;
        currentCombination.combinationText.fontStyle = FontStyle.Bold;
    }

    void DifficultySetting()
    {
        switch(difficultyState)
        {
            case Difficulty.EASY:
                numCombinations = 3;
                timeTillComboReset = 10;
                break;
            case Difficulty.MEDIUM:
                numCombinations = 4;
                timeTillComboReset = 7;
                break;
            case Difficulty.HARD:
                numCombinations = 5;
                timeTillComboReset = 5;
                break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            onCombinationRelock();
        }

        if(currentCombinationIndex > 0)
        {
            TimeCounter();
        }
    }

    void onCombinationUnlock()
    {
        var currentCombination = combinations[currentCombinationIndex];
        // combination doesn't unlock if guessed value is not randomized value
        if(currentCombination.guessedValue != currentCombination.randomizedValue) return;
        
        // disable size fitter to maintain combination position in grid
        if(sizeFitter.enabled == true) sizeFitter.enabled = false;
        

        Debug.Log("Unlocked combination " + currentCombinationIndex);
        currentCombination.gameObject.SetActive(false);
        
        //sender.gameObject.SetActive(false);

        currentCombinationIndex++;
        
        if(currentCombinationIndex >= combinations.Count) return;

        currentCombination = combinations[currentCombinationIndex];
        currentCombination.isCurrent = true;
        // resets new current combination to white
        currentCombination.combinationImg.color = Color.white;

        currentCombination.combinationText.color = Color.red;
        currentCombination.combinationText.fontStyle = FontStyle.Bold;


        // Reset timer
        SECOND = timeTillComboReset;
    }

    void onCombinationRelock()
    {
        if(currentCombinationIndex >= combinations.Count) return;

        if(currentCombinationIndex <= 0)
        {
            currentCombinationIndex = 0;
            return;
        }

        var currentCombination = combinations[currentCombinationIndex];
        currentCombination.combinationImg.color = Color.white;
        currentCombination.combinationText.color = Color.black;
        currentCombination.combinationText.fontStyle = FontStyle.Normal;
        currentCombination.guessedValue = 0;
        currentCombination.combinationText.text = 0.ToString();
        currentCombination.isCurrent = false;

        currentCombinationIndex--;

        currentCombination = combinations[currentCombinationIndex];
        currentCombination.gameObject.SetActive(true);

        currentCombination.isCurrent = true;
        currentCombination.combinationImg.color = Color.white;
        currentCombination.combinationText.color = Color.red;
        currentCombination.combinationText.fontStyle = FontStyle.Normal;
    }
    
    void TimeCounter()
    {
        if (timeCounter >= 1)
        {
            SECOND--;
            if (SECOND <= 0)
            {
                onCombinationRelock();
                SECOND = timeTillComboReset;
            }
            timeCounter = 0;
        }
        else
        {
            timeCounter += Time.deltaTime;
        }
    }
}