using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarScript : MonoBehaviour
{

    private Player player;
    private float currScale;

    // Start is called before the first frame update
    void Start()
    {
        
        player = player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        currScale = player.GetStamina() / player.GetMaxStamina();
        transform.localScale = new Vector3(currScale, 1, 1);

    }
}
