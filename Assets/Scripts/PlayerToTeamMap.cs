using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerNumber { ONE = 1, TWO = 2, THREE = 3, FOUR = 4 }
[System.Serializable]
public enum TeamNumber { NULL = 0, ONE = 1, TWO = 2 }

public static class PlayerToTeamMap
{
    // Map player numbers to their team
    // 1 = team 1, 2 = team 2
    private static Dictionary<PlayerNumber, TeamNumber> teamMap = new Dictionary<PlayerNumber, TeamNumber>()
    {
        { PlayerNumber.ONE, TeamNumber.NULL },
        { PlayerNumber.TWO, TeamNumber.NULL },
        { PlayerNumber.THREE, TeamNumber.NULL },
        { PlayerNumber.FOUR, TeamNumber.NULL }
    };

    public static bool TeamsAreReady()
    {
        return IsTeamFull(TeamNumber.ONE) && IsTeamFull(TeamNumber.TWO);
    }

    private static bool IsTeamFull(TeamNumber team) {
        int count = 0;
        foreach (TeamNumber t in teamMap.Values) {
            if (t == team) {
                count++;
            }
        }
        return count >= 2;
    }

    public static bool AssignPlayerToTeam(PlayerNumber player, TeamNumber team) {
        if (team == TeamNumber.NULL) { return false; }

        // If team is not full, assign to team
        if (!IsTeamFull(team)) {
            teamMap[player] = team;
            return true;
        }

        return false;
    }

    public static void ResetPlayerTeam(PlayerNumber player)
    {
        teamMap[player] = TeamNumber.NULL;
    }

    public static TeamNumber GetPlayerTeam(PlayerNumber player) {
        return teamMap[player];
    }
}
