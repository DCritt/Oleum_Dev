using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IInteractable
{

    public Player player { get; set; }
    string GetInteractText(Player player);
    void Interact(Player player);
    void OnStart(Player player);
    void OnEnd(Player player);
    

}
