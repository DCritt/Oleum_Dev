using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{

    [SerializeField] private Player player;
    private float currScale;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        currScale = player.GetHealth() / player.GetMaxHealth();
        transform.localScale = new Vector3(currScale, 1, 1);

    }
}
