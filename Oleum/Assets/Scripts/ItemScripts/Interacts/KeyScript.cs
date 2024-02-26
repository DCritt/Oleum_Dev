using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : ItemInteractScript
{

    public Door door;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Door"))
        {

            door = collision.GetComponent<Door>();

        }

    }

    public override void Interact()
    {

        door?.Unlock();

    }

}
