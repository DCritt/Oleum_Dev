using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{

    [SerializeField] private GameObject InteractButtons;
    [SerializeField] private Text _interactText;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private Image[] hotbarSlots = new Image[4] { null, null, null, null };
    [SerializeField] private Image[] hotbarSelects = new Image[4] { null, null, null, null };
    [SerializeField] private GameObject mainObjTracker;
    [SerializeField] private GameObject sideObjTracker;
    private ObjectiveScript objectives;
    private Dropdown powerCellPipeDropdown;

    public TransportLocationsScript GetInteractButtons()
    {

        return InteractButtons.GetComponent<TransportLocationsScript>();

    }

    public void ActivateInteractButtons()
    {

        InteractButtons.SetActive(true);

    }

    public void DeactivateInteractButtons()
    {

        InteractButtons.SetActive(false);

    }

    public void UpdateObjectives(int mainAmt, int sideAmt)
    {

        objectives.ProgressMain(mainAmt);
        objectives.ProgressSide(sideAmt);

    }

    public void SwitchInteractActive()
    {

        _interactText.enabled = true;

    }

    public void SwitchInteractDeactive()
    {

        _interactText.enabled = false;

    }

    public void SetInteractText(string text)
    {

        _interactText.text = "[E] " + text;

    }

    public void SetSelect(int next)
    {

        hotbarSelects[next].enabled = true;

    }

    public void SetSelect(int curr, int next)
    {
        if (next == 4)
        {

            return;

        }
        if (curr == 4)
        {

            SetSelect(next);

        }
        else
        {

            hotbarSelects[curr].enabled = false;
            hotbarSelects[next].enabled = true;

        }

    }

    public void ResetSelect()
    {

        for (int i = 0; i < 4; i++)
        {

            hotbarSelects[i].enabled = false;

        }

    }

    public void SetImage(int slot, Item item)
    {

        if (item == null)
        {

            hotbarSlots[slot].sprite = null;
            hotbarSlots[slot].enabled = false;

        }
        else
        {

            hotbarSlots[slot].sprite = item.data.icon;
            hotbarSlots[slot].enabled = true;

        }

    }

    private void Awake()
    {

        objectives = new ObjectiveScript(mainObjTracker, sideObjTracker);

    }

    // Start is called before the first frame update
    void Start()
    {

        //_interactText = GameObject.FindGameObjectWithTag("InteractText");
        SwitchInteractDeactive();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
