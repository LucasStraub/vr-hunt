using System.Collections;
using UnityEngine;

public class HuntableCaughtState : HuntableBaseState
{
    public override void EnterState(Huntable huntable)
    {
        huntable.DestroySelf();
    }

    public override void ExitState(Huntable huntable) { }

    public override void UpdateState(Huntable huntable) { }


}