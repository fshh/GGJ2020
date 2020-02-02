using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class HelpScreenManager : MonoBehaviour
{
    private bool active;
    private GameSetupManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameSetupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) { return; }

        foreach (Player p in ReInput.players.Players)
        {
            if (p.GetButtonDown("Cancel"))
            {
                manager.ChangeToScreen(GameSetupManager.Screen.TEAMSELECT);
            }
        }
    }

    public void SetAsActive(bool active)
    {
        StartCoroutine(SetActiveNextFrame(active));
    }

    private IEnumerator SetActiveNextFrame(bool active)
    {
        yield return new WaitForFixedUpdate();
        this.active = active;
    }
}
