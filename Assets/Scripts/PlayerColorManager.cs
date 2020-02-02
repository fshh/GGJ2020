using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerColorManager : MonoBehaviour
{
    public Color Team1;
    public Color Team2;

    // Sets the colors of the given player object to the colors associated with the given player number
    public void SetColors(GameObject player, int pNum) {
        SpriteRenderer bodySprite = player.GetComponentInChildren<SpriteRenderer>();
        TextMeshProUGUI[] texts = player.GetComponentsInChildren<TextMeshProUGUI>();

        Color col = GetPlayerColor(pNum);

        bodySprite.color = col;
        foreach (TextMeshProUGUI t in texts) {
            t.color = col;
        }
    }

    public Color GetPlayerColor(int pNum) {
        return PlayerToTeamMap.GetPlayerTeam((PlayerNumber)pNum) == TeamNumber.ONE ? Team1 : Team2;
    }
}
