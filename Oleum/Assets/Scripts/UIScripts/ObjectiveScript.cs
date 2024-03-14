using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript : MonoBehaviour
{

    [SerializeField] private Text progress;

    // Start is called before the first frame update
    void Start()
    {

        progress.text = GameManagerScript.instance.mainObjective.GetProgress() + "/" + GameManagerScript.instance.mainObjective.GetMaxProgress();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateText()
    {

        progress.text = GameManagerScript.instance.mainObjective.GetProgress() + "/" + GameManagerScript.instance.mainObjective.GetMaxProgress();

    }

}
