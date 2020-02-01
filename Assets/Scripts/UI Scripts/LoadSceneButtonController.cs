using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButtonController : MonoBehaviour
{
    public string scene;
    public void LoadNewScene() {
        SceneManager.LoadScene(scene);
    }
}
