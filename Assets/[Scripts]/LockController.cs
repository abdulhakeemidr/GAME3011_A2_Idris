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
    float timeTillComboReset;
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
                break;
            case Difficulty.MEDIUM:
                numCombinations = 3;
                break;
            case Difficulty.HARD:
                numCombinations = 4;
                break;
        }
    }

    void onCombinationUnlock()
    {
        var currentCombination = combinations[currentCombinationIndex];
        
        if(currentCombination.guessedValue != currentCombination.randomizedValue) return;
        if(sizeFitter.enabled == true) sizeFitter.enabled = false;
        
        Debug.Log("Unlocked combination " + currentCombinationIndex);
        currentCombination.gameObject.SetActive(false);
        
        //sender.gameObject.SetActive(false);

        currentCombinationIndex++;

        currentCombination = combinations[currentCombinationIndex];
        // resets new current combination to white
        currentCombination.combinationImg.color = Color.white;

        currentCombination.combinationText.color = Color.red;
        currentCombination.combinationText.fontStyle = FontStyle.Bold;
        currentCombination.isCurrent = true;
    }
}