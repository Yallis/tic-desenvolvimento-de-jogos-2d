using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    Renderer bulletRenderer;
    SFXController sfxController;

    private void Start() {
        bulletRenderer = GetComponent<Renderer>();
        sfxController = FindObjectOfType<SFXController>();
        sfxController.PlayShot();
    }

    private void Update() {
        if(!bulletRenderer.isVisible)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
            sfxController.PlayEnemyDeath();
        }

        Destroy(gameObject);
    }
}
