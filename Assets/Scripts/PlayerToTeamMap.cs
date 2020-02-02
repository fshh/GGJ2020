using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerToTeamMap
{
    // Map player numbers to their team
    // 1 = team 1, 2 = team 2
    private static Dictionary<PlayerInput.PlayerNumber, int> map = new Dictionary<PlayerInput.PlayerNumber, int>(4);

    private static bool IsTeamFull(int team) {
        int count = 0;
        foreach (int t in map.Values) {
            if (t == team) {
                count++;
            }
        }
        return count >= 2;
    }

    public static bool AssignPlayerToTeam(PlayerInput.PlayerNumber player, int team) {
        if (team != 1 && team != 2) { return false; }

        // If team is not full, assign to team
        if (!IsTeamFull(team)) {
            map[player] = team;
            return true;
        }

        return false;
    }

    public static int GetPlayerTeam(PlayerInput.PlayerNumber player) {
        return map[player];
    }
}
