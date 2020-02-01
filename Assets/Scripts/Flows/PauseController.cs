using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool paused;
    public string pauseButton;
    public Canvas pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseButton)) {
            togglePause();
        }
    }

    public void togglePause() {
        if (paused) {
            pauseMenuUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        } else {
            Time.timeScale = 0.0f;
            pauseMenuUI.gameObject.SetActive(true);
        }
        paused = !paused;
    }
}
