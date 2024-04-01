using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript
{

    private Text mainProgressText;
    private Text sideProgressText;
    private Objective mainObjective;
    private Objective sideObjective;
    private GameObject mainObjectiveTracker;
    private GameObject sideObjectiveTracker;
    private int mainProgress = 0;
    private int sideProgress = 0;

    public ObjectiveScript(GameObject mainObjectiveTracker, GameObject sideObjectiveTracker, Objective mainObjective, Objective sideObjective)
    {

        this.mainObjectiveTracker = mainObjectiveTracker;
        this.sideObjectiveTracker = sideObjectiveTracker;
        this.mainObjective = mainObjective;
        this.sideObjective = sideObjective;
        mainProgressText = mainObjectiveTracker.GetComponent<Text>();
        sideProgressText = sideObjectiveTracker.GetComponent<Text>();

    }

    public void ProgressMain(int progress)
    {
    
        if ((mainProgress + progress) > mainObjective.GetMaxProgress())
        {

            mainProgress = mainObjective.GetMaxProgress();

        }
        else
        {

            mainProgress += progress;

        }

        UpdateText();

    }

    public void ProgressSide(int progress)
    {

        if ((sideProgress + progress) > sideObjective.GetMaxProgress())
        {

            sideProgress = sideObjective.GetMaxProgress();

        }
        else
        {

            sideProgress += progress;

        }

        UpdateText();

    }

    public void UpdateText()
    {

        mainProgressText.text = mainProgress + "/" + mainObjective.GetMaxProgress();
        sideProgressText.text = sideProgress + "/" + sideObjective.GetMaxProgress();

    }

}
