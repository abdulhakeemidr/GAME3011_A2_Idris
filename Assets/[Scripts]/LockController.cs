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
    List<GameObject> combinations;
    GameObject lockPickObj;


    int numCombinations;

    void Awake()
    {
        parent = transform.parent.gameObject;
    }

    void Start()
    {
        sizeFitter = GetComponent<ContentSizeFitter>();

        DifficultySetting();

        for(int i = 0; i < numCombinations; i++)
        {
            GameObject newObj = Instantiate(combinationPrefab, this.transform);
            combinations.Add(newObj);
        }

        //sizeFitter.enabled = false;
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
                numCombinations = 3;
                break;
        }
    }
}