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
            GameObject player = Instantiate(playerPrefab, spawns.GetChild(ii).transform.position, Quaternion.identity);
            player.name = "Player " + (ii + 1);
            player.GetComponent<PlayerInput>().playerNumber = (PlayerInput.PlayerNumber)(ii + 1);
            colorManager.SetColors(player, ii + 1);
        }
    }


}
