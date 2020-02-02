using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TeamSelectManager : MonoBehaviour
{
    public GameObject pressToContinue;

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

        pressToContinue.SetActive(PlayerToTeamMap.TeamsAreReady());

        foreach (Player p in ReInput.players.Players)
        {
            if (p.GetButtonDown("Help"))
            {
                AkSoundEngine.PostEvent("SFX_UI_Confirm", gameObject);
                manager.ChangeToScreen(GameSetupManager.Screen.HELP);
            }
        }

        if (PlayerToTeamMap.TeamsAreReady())
        {
            foreach (Player p in ReInput.players.Players)
            {
                if (p.GetButtonDown("Confirm"))
                {
                    AkSoundEngine.PostEvent("SFX_UI_Confirm", gameObject);
                    manager.ChangeToScreen(GameSetupManager.Screen.LEVELSELECT);
                }
            }
        }
    }

    public void GoBack()
    {
        manager.ChangeToScreen(GameSetupManager.Screen.MAINMENU);
    }

    public void SetAsActive(bool active)
    {
        StartCoroutine(SetActiveNextFrame(active));
    }

    private IEnumerator SetActiveNextFrame(bool active)
    {
        yield return new WaitForFixedUpdate();
        this.active = active;
        SetPlayerSelectScriptsEnabled(active);
    }

    private void SetPlayerSelectScriptsEnabled(bool enabled)
    {
        PlayerSelect[] scripts = Resources.FindObjectsOfTypeAll<PlayerSelect>();
        foreach (PlayerSelect script in scripts)
        {
            script.enabled = enabled;
        }
    }
}
