using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static readonly int MAX_PLAYERS = 4;

    public static int NumberOfPlayers { private set; get; } = 1;

    /// <summary>
    /// Sets the number of players in the game to the given number.
    /// </summary>
    /// <param name="num">The number of players</param>
    public static void SetNumberOfPlayers(int num)
    {
        if (num > MAX_PLAYERS || num <= 0)
        {
            throw new System.ArgumentException("Given invalid number of players: " + num);
        }
        NumberOfPlayers = num;
    }
}
