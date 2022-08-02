using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Huntable : XRBaseInteractable
{
    // States
    public static Action<HuntableBaseState> OnHuntableStateChanged;
    public HuntableIdleState IdleState => _idleState;
    private readonly HuntableIdleState _idleState = new();
    public HuntableCatchingState CatchingState => _catchingState;
    private readonly HuntableCatchingState _catchingState = new();
    public HuntableCaughtState CaughtState => _caughtState;
    private readonly HuntableCaughtState _caughtState = new();

    // Components
    public Animator AnimatorUI => _animatorUI;
    [SerializeField] private Animator _animatorUI;
    public HuntSettings HuntSettings => _huntSettings;
    [SerializeField] private HuntSettings _huntSettings;

    private HuntableBaseState _currentState;

    private new void Awake()
    {
        base.Awake();

        if (_huntSettings == null)
            _huntSettings = new();
    }


    private void Start()
    {
        SetState(IdleState);
        AnimatorUI.SetFloat("Timer", 1 / HuntSettings.CatchTime);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SetState(HuntableBaseState state)
    {
        if (_currentState == state)
            return;

        if (_currentState != null)
            _currentState.ExitState(this);

        _currentState = state;
        _currentState.EnterState(this);

        OnHuntableStateChanged?.Invoke(_currentState);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}