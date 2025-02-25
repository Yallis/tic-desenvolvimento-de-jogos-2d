using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    public AudioClip getItemSound;
    public AudioClip jumpSound;
    public AudioClip shotSound;
    public AudioClip enemyDeathSound;
    public AudioClip playerDeathSound;
    public AudioClip winSound;

    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGetItem() {
        audioSource.PlayOneShot(getItemSound);
    }

    public void PlayJumpSound() {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayShot() {
        audioSource.PlayOneShot(shotSound);
    }

    public void PlayEnemyDeath() {
        audioSource.PlayOneShot(enemyDeathSound);
    }

    public void PlayPlayerDeath() {
        audioSource.PlayOneShot(playerDeathSound);
    }

    public void PlayWin() {
        audioSource.PlayOneShot(winSound);
    }
}
