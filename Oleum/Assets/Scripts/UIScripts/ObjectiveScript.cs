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
    private static int mainProgress = 0;
    private static int sideProgress = 0;
    private static List<ObjectiveScript> instanceList = new List<ObjectiveScript>();

    public ObjectiveScript(GameObject mainObjectiveTracker, GameObject sideObjectiveTracker, Objective mainObjective, Objective sideObjective)
    {

        this.mainObjectiveTracker = mainObjectiveTracker;
        this.sideObjectiveTracker = sideObjectiveTracker;
        this.mainObjective = mainObjective;
        this.sideObjective = sideObjective;
        instanceList.Add(this);
        mainProgressText = mainObjectiveTracker.GetComponent<Text>();
        sideProgressText = sideObjectiveTracker.GetComponent<Text>();

    }

    public void ProgressMain(int progress)
    {
    
        if ((mainProgress + progress) > mainObjective.GetMaxProgress())
        {

            mainProgress = mainObjective.GetMaxProgress();
            CompleteMain();

        }
        else
        {

            mainProgress += progress;

        }

        UpdateAll();

    }

    public void ProgressSide(int progress)
    {

        if ((sideProgress + progress) > sideObjective.GetMaxProgress())
        {

            sideProgress = sideObjective.GetMaxProgress();
            CompleteSide();

        }
        else
        {

            sideProgress += progress;

        }

        UpdateAll();

    }

    public void UpdateAll()
    {

        for (int i = 0; i < instanceList.Count; i++)
        {

            instanceList[i].UpdateText();

        }
    }
    public void UpdateText()
    {

        mainProgressText.text = mainProgress + "/" + mainObjective.GetMaxProgress();
        sideProgressText.text = sideProgress + "/" + sideObjective.GetMaxProgress();

    }



    public void CompleteMain()
    {



    }

    public void CompleteSide()
    {



    }

}
