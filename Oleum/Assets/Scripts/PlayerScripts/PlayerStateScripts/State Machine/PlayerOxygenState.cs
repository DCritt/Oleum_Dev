using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygenState
{

    protected Player player;
    protected PlayerStateMachine playerStateMachine;

    public PlayerOxygenState(Player player, PlayerStateMachine playerStateMachine)
    {

        this.player = player;
        this.playerStateMachine = playerStateMachine;

    }

    public virtual void EnterState()
    {



    }

    public virtual void ExitState()
    {



    }

    public virtual void FrameUpdate()
    {



    }

    public virtual void PhysicsUpdate()
    {



    }

}
