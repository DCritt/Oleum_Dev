using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    { 

        Vector3 pos = new Vector3((player.transform.position.x), (player.transform.position.y), -10);

        transform.position = pos;

    }
}
