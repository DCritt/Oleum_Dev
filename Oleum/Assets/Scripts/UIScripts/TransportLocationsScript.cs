using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TransportLocationsScript : MonoBehaviour
{

    private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject buttonPrefab;

    public void SpawnTransportLocationButton(string name, UnityEngine.Events.UnityAction transport)
    {

        buttons.Add(Instantiate(buttonPrefab, panel.transform.position, panel.transform.rotation, panel.transform));
        buttons[buttons.Count - 1].GetComponent<Button>().onClick.AddListener(transport);
        buttons[buttons.Count - 1].transform.GetChild(0).GetComponent<Text>().text = name;

    }

    public void ResetInteractButtons()
    {

        for (int i = 0; i < buttons.Count; i++)
        {

            Destroy(buttons[i]);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
