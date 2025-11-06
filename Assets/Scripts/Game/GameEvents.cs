using System;
using SOFile;

public static class GameEvents
{
    public static Action OnFlipCountdownEnd;
    public static Action OnNewGameStarted;
    public static Action<CardDataSO> OnEndOfRound;

    public static Action OnUserHavePriority;

    public static Action<IPlayer> OnClickCardOnTable;
}