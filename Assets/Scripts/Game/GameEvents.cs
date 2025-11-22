using System;
using SOFile;

public static class GameEvents
{

    public static Action OnFlipCountdownEnd;
    public static Action OnNewGameStarted;
    public static Action<CardDataSO> OnEndOfRound;

    public static Action OnUserHavePriority;

    public static Action<IPlayer> OnClickCardOnTable;
    
    public static Action OnPlayerAttackChosen;
    public static Action OnComputerAttackChosen;
    public static Action OnAttackRoundEnd;
    public static Action<int> OnAllAttackEnd;
    public static Action OnDefendInput;
    public static Action<int> OnComputerAttackEnd;

    public static Action<IButtonListener> OnAttackOrKeepButtonClicked;
    public static Action<IPlayer> OnSetDefencePlayer;
}