﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject soloist1;
    public GameObject soloist2;
    public Canvas gameOverUI;
    public float liftSpeed;
    public float maxY;
    private int winner;
    private int leader;
    private float team1Cogs;
    private float team2Cogs;

    // Start is called before the first frame update
    void Start()
    {
        winner = 0;
        leader = 0;
        team1Cogs = 0.0f;
        team2Cogs = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (soloist1.transform.position.y >= maxY) {
            winner = 1;
        } else if (soloist2.transform.position.y >= maxY) {
            winner = 2;
        }

        if (winner > 0) {
            EndGame();
        }

        LiftSoloist(soloist1, team1Cogs);
        LiftSoloist(soloist2, team2Cogs);
        float biggestY = Mathf.Max(soloist1.transform.position.y, soloist2.transform.position.y);

        if (leader == 0 && biggestY == soloist1.transform.position.y && biggestY != soloist2.transform.position.y)
		{
            leader = 1;
            AkSoundEngine.PostEvent("Set_State_Rock_Winning", gameObject);
        } else if (leader == 0 && biggestY == soloist2.transform.position.y && biggestY != soloist1.transform.position.y)
		{
            leader = 2;
            AkSoundEngine.PostEvent("Set_State_SynthWinning", gameObject);
        } else if (leader == 1 && biggestY != -8.5f && biggestY == soloist2.transform.position.y)
		{
            leader = 2;
            AkSoundEngine.PostEvent("Set_State_SynthWinning", gameObject);
        } else if (leader == 2 && biggestY != -8.5f && biggestY == soloist1.transform.position.y)
		{
            leader = 1;
            AkSoundEngine.PostEvent("Set_State_Rock_Winning", gameObject);
        }
    }

    public int GetWinner() {
        return winner;
    }

    public void AddCog(int team) {
        if (team == 1) {
            team1Cogs += 1.0f;
        } else {
            team2Cogs += 1.0f;
        }
    }

    public void SubtractCog(int team) {
        if (team == 1) {
            team1Cogs -= 1.0f;
        } else {
            team2Cogs -= 1.0f;
        }
    }

    private void EndGame() {
        Time.timeScale = 0.0f;
        gameOverUI.gameObject.SetActive(true);
    }

    private void LiftSoloist(GameObject soloist, float scalar) {
        soloist.transform.Translate(Vector2.up * scalar * liftSpeed * Time.deltaTime);
    }
}
