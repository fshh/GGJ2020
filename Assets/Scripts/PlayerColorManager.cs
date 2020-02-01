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
        SpriteRenderer bodySprite = player.GetComponent<SpriteRenderer>();
        TextMeshProUGUI[] texts = player.GetComponentsInChildren<TextMeshProUGUI>();

        Color col = pNum <= 2 ? Team1 : Team2;

        bodySprite.color = col;
        foreach (TextMeshProUGUI t in texts) {
            t.color = col;
        }
    }

    public Color GetPlayerColor(int pNum) {
        return pNum <= 2 ? Team1 : Team2;
    }
}
