using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMoveable
{
    
    Rigidbody2D RB { get; set; }
    void MovePlayer(Vector2 velocity);

}
