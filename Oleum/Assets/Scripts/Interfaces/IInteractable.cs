using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    public Player player { get; set; }
    string GetInteractText(Player player);
    void Interact(Player player);
    

}
