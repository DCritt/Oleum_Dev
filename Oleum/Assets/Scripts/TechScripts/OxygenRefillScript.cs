using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRefillScript : MonoBehaviour
{

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            player = collision.GetComponent<Player>();
            player.StateMachine.ChangeState(player.OxygenGainState);

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            player = collision.GetComponent<Player>();
            player.StateMachine.ChangeState(player.OxygenDrainState);

        }

    }
}
