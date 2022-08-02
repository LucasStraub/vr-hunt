using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HuntableCatchingState : HuntableBaseState
{
    private Huntable _huntable;
    private float _timer;

    public override void EnterState(Huntable huntable)
    {
        _huntable = huntable;
        _timer = 0;
        huntable.AnimatorUI.SetBool("Catching", true);
        huntable.hoverExited.AddListener(StopLoading);
    }

    public override void ExitState(Huntable huntable)
    {
        huntable.AnimatorUI.SetBool("Catching", false);
        huntable.hoverExited.RemoveListener(StopLoading);

    }

    public override void UpdateState(Huntable huntable)
    {
        _timer += Time.deltaTime;
        if (_timer > huntable.HuntSettings.CatchTime)
            huntable.SetState(huntable.CaughtState);
    }

    private void StopLoading(HoverExitEventArgs enterEvent)
    {
        _huntable.SetState(_huntable.IdleState);
    }
}
