using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int numPlayers = 4;
    public PlayerColorManager colorManager;

    void OnEnable()
    {
        SpawnPlayers();
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetButtonDown("Restart")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }

    private void SpawnPlayers()
    {
        Transform spawns = GameObject.Find("SpawnPoints").transform;
        GameObject playerPrefab = Resources.Load<GameObject>("Player");
        for (int ii = 0; ii < numPlayers; ii++)
        {
            // Instantiate player
            GameObject player = Instantiate(playerPrefab, spawns.GetChild(ii).transform.position, Quaternion.identity);

            // Set player name, number, and colors
            player.name = "Player " + (ii + 1);
            player.GetComponent<PlayerInput>().playerNumber = (PlayerNumber)(ii + 1);
            colorManager.SetColors(player, ii + 1);

            // Set player layer and collision mask
            player.layer = LayerMask.NameToLayer(ii < numPlayers / 2 ? "Team1" : "Team2");

            // Un-comment to enable collision between teams
            /* 
            int enemyLayer = 1 << LayerMask.NameToLayer(ii < numPlayers / 2 ? "Team2" : "Team1");
            player.GetComponent<Controller2D>().collisionMask |= enemyLayer;
            */
        }
    }


}
