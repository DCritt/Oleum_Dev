using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBarScript : MonoBehaviour
{

    private Player player;
    private float currScale;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        currScale = player.GetOxygen() / player.GetMaxOxygen();
        transform.localScale = new Vector3(currScale, 1, 1);

        
    }
}
