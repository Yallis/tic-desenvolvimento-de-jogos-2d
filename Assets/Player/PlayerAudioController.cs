using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour {

    public AudioClip getItemSound;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGetItem() {
        audioSource.PlayOneShot(getItemSound);
    }
}
