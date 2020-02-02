using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSetupManager : MonoBehaviour
{
    public float transitionTime = 0.5f;
    //public AudioSource menuMusic;
    private MainMenuManager mainMenu;
    private TeamSelectManager teamSelect;
    private HelpScreenManager helpScreen;
    private LevelSelectManager levelSelect;

    public enum Screen { MAINMENU, TEAMSELECT, LEVELSELECT, HELP }
    private Screen currentScreen = Screen.MAINMENU;
    private RectTransform rect;
    private Tween moveTween;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        // initialize moveTween to some value
        moveTween = rect.DOScale(1f, 0.001f);

        mainMenu = FindObjectOfType<MainMenuManager>();
        teamSelect = FindObjectOfType<TeamSelectManager>();
        helpScreen = FindObjectOfType<HelpScreenManager>();
        levelSelect = FindObjectOfType<LevelSelectManager>();

        mainMenu.SetAsActive(true);
        teamSelect.SetAsActive(false);
        helpScreen.SetAsActive(false);
        levelSelect.SetAsActive(false);

        //menuMusic.loop = true;
        //menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToScreen(Screen newScreen)
    {
        if (moveTween.IsPlaying()) { return; }

        Vector2 targetPos = Vector2.zero;
        switch (newScreen)
        {
            case Screen.MAINMENU:
                targetPos = Vector2.zero;
                break;
            case Screen.TEAMSELECT:
                targetPos = new Vector2(-1920, 0);
                break;
            case Screen.LEVELSELECT:
                targetPos = new Vector2(-1920 * 2, 0);
                break;
            case Screen.HELP:
                targetPos = new Vector2(-1920, -1080);
                break;
        }
        moveTween = GetComponent<RectTransform>().DOAnchorPos(targetPos, transitionTime);
        moveTween.Play();

        // Enable/disable scripts
        mainMenu.SetAsActive(newScreen == Screen.MAINMENU);
        teamSelect.SetAsActive(newScreen == Screen.TEAMSELECT);
        helpScreen.SetAsActive(newScreen == Screen.HELP);
        levelSelect.SetAsActive(newScreen == Screen.LEVELSELECT);
    }
}
