using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HuntableIdleState : HuntableBaseState
{
    private Huntable _huntable;

    public override void EnterState(Huntable huntable)
    {
        _huntable = huntable;
        huntable.hoverEntered.AddListener(StartLoading);
    }

    public override void ExitState(Huntable huntable)
    {
        huntable.hoverEntered.RemoveListener(StartLoading);
    }

    public override void UpdateState(Huntable huntable) { }

    private void StartLoading(HoverEnterEventArgs enterEvent)
    {
        if (Vector3.Distance(enterEvent.interactorObject.transform.position, _huntable.transform.position) > _huntable.HuntSettings.CatchDistance)
            return;
        _huntable.SetState(_huntable.CatchingState);
    }
}
