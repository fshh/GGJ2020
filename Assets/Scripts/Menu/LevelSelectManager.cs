using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;

public class LevelSelectManager : MonoBehaviour
{
    public float cursorSpeed = 10f;
    public RectTransform cursor;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    private Bounds menuBounds;
    private Player player;
    private bool active;
    private GameSetupManager manager;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        menuBounds = new Bounds(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(1920, 1080));
        player = ReInput.players.GetPlayer(0);
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

        cursor.position += (Vector3)player.GetAxis2DRaw("Move Horizontal", "Move Vertical") * cursorSpeed;
        cursor.position = menuBounds.ClosestPoint(cursor.position);

        if (player.GetButtonDown("Confirm"))
        {
            PointerEventData data = new PointerEventData(eventSystem);
            data.position = cursor.position;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(data, results);
            if (results.Count > 0)
            {
                LoadSceneButtonController sceneLoader = results[0].gameObject.GetComponent<LoadSceneButtonController>();
                if (sceneLoader) { sceneLoader.LoadNewScene(); }
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
        cursor.gameObject.SetActive(active);
    }
}
