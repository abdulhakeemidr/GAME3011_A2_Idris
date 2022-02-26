using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public SkillLevel playerSkill = SkillLevel.BEGINNER;
    CameraController playerCamControl;
    [SerializeField]
    public GameObject UICanvas;
    [SerializeField]
    GameObject lockPickCanvasInst;

    string skillLevelTxt = "";
    int timerIncrease = 0;

    void Start()
    {
        playerCamControl = GetComponentInChildren<CameraController>();
        SetSkillLevel();

        //UICanvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        Text playerSkillTxt = UICanvas.transform.GetChild(0).GetComponent<Text>();
        playerSkillTxt.text = skillLevelTxt;

        //lockPickCanvasInst = LockUIManager.instance.canvasParentPrefab.gameObject;
        lockPickCanvasInst.SetActive(false);
    }

    void SetSkillLevel()
    {
        switch(playerSkill)
        {
            case SkillLevel.BEGINNER:
                skillLevelTxt = "Beginner";
                timerIncrease = 0;
                break;
            case SkillLevel.INTERMEDIATE:
                skillLevelTxt = "Intermediate";
                timerIncrease = 2;
                break;
            case SkillLevel.MASTER:
                skillLevelTxt = "Master";
                timerIncrease = 4;
                break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //lockPickCanvasInst = Instantiate(lockPickCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            lockPickCanvasInst.SetActive(true);
            var lockController = LockUIManager.instance.lockControllerObj.GetComponent<LockController>();
            lockController.timeTillComboReset += timerIncrease;
            lockController.difficultyState = Difficulty.MEDIUM;

            playerCamControl.enabled = false;
        }

        if(lockPickCanvasInst.activeSelf == false)
        {
            playerCamControl.enabled = true;
        }
    }
}
