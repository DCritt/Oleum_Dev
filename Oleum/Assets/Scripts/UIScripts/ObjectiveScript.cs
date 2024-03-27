using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript
{

    private Text mainProgress;
    private Text sideProgress;
    private Objective mainObjective;
    private Objective sideObjective;
    private GameObject mainObjectiveTracker;
    private GameObject sideObjectiveTracker;

    public ObjectiveScript(GameObject mainObjectiveTracker, GameObject sideObjectiveTracker, Objective mainObjective, Objective sideObjective)
    {

        this.mainObjectiveTracker = mainObjectiveTracker;
        this.sideObjectiveTracker = sideObjectiveTracker;
        this.mainObjective = mainObjective;
        this.sideObjective = sideObjective;
        mainProgress = mainObjectiveTracker.GetComponent<Text>();
        sideProgress = sideObjectiveTracker.GetComponent<Text>();

    }

    public void UpdateText()
    {

        mainProgress.text = mainObjective.GetProgress() + "/" + mainObjective.GetMaxProgress();
        sideProgress.text = sideObjective.GetProgress() + "/" + sideObjective.GetMaxProgress();

    }

}
