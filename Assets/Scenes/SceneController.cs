using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    string activeSceneName;

    void Start() {
        activeSceneName = SceneManager.GetActiveScene().name;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(activeSceneName == "HomeScene") {
                LoadScene("Level 1");
            }
            if(activeSceneName == "GameOver") {
                LoadScene("HomeScene");
            }
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
