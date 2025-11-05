using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: Implement this later, just use mouse and buttons for now
public abstract class IGameState
{
    protected int xIndex;
    protected int yIndex;
    protected Dictionary<int, List<Action>> actions;
    
    
    // MainScreenState
    // SelectVsState
    // ExitGameState


    // PlayState,
    // WaitingState,
    // FailedState,
    // CardAvailableState

    // CardCapturedState ( same as SelectActionState),
    // OpponentCapturedState

    // ResultState,


}
