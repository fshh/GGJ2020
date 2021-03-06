﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    public Sprite team1Win;
    public Sprite team2Win;
    private Image myImage;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        // Filler conditional until game controller is built
        if (gameController.GetComponent<GameController>().GetWinner() == 1) {
            myImage.sprite = team1Win;
            AkSoundEngine.PostEvent("Rock_Solo_Win", gameObject);
        } else {
            myImage.sprite = team2Win;
            AkSoundEngine.PostEvent("Synth_Solo_Win", gameObject);
        }
    }
}
